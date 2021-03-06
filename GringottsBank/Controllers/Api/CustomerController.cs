using AutoMapper;
using GringottsBank.Authentication;
using GringottsBank.Models;
using GringottsBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBank.Controllers.Api
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper; 
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, 
            UserManager<ApplicationCustomer> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterOrUpdateCustomer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(newCustomer);
            }
            try
            {
                var userExists = await _userManager.FindByNameAsync(newCustomer.EmailID);
                if (userExists != null)
                {
                    _logger.LogError("Customer with EmailID : " + newCustomer.EmailID + " exists");
                    return BadRequest("Customer with EmailID : " + newCustomer.EmailID + " exists");
                }
                    

                ApplicationCustomer appCustomer = new ApplicationCustomer()
                {
                    Email = newCustomer.EmailID,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = newCustomer.EmailID
                    
                };
                var result = await _userManager.CreateAsync(appCustomer, newCustomer.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("User Creation Failed" + result.ToString());
                    return StatusCode(StatusCodes.Status500InternalServerError, "User Creation Failed! Please Try Again");
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(appCustomer, UserRoles.User);
                }
                var customer = _mapper.Map<Customer>(newCustomer);
                var customerToBeRegistered = await _customerService.RegisterCustomer(customer);
                var customerToSend = _mapper.Map<GetCustomer>(customerToBeRegistered);
                return Ok(customerToSend);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterCustomerAsAdmin([FromBody] RegisterOrUpdateCustomer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(newCustomer);
            }
            try
            {
                var userExists = await _userManager.FindByNameAsync(newCustomer.EmailID);
                if (userExists != null)
                {
                    _logger.LogError("Customer with EmailID : " + newCustomer.EmailID + " exists");
                    return BadRequest("Customer with EmailID : " + newCustomer.EmailID + " exists");
                }                    

                ApplicationCustomer appCustomer = new ApplicationCustomer()
                {
                    Email = newCustomer.EmailID,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = newCustomer.EmailID
                };
                var result = await _userManager.CreateAsync(appCustomer, newCustomer.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("User Creation Failed" + result.ToString());
                    return StatusCode(StatusCodes.Status500InternalServerError, "User Creation Failed! Please Try Again");
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(appCustomer, UserRoles.Admin);
                }
                var customer = _mapper.Map<Customer>(newCustomer);
                var customerToBeRegistered = await _customerService.RegisterCustomer(customer);
                var customerToSend = _mapper.Map<GetCustomer>(customerToBeRegistered);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginCustomer loginCustomer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(loginCustomer);
            }
            try
            {
                var user = await _userManager.FindByNameAsync(loginCustomer.EmailID);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginCustomer.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                _logger.LogError("User is unauthorized");
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                var customersToSend = _mapper.Map<IList<GetCustomer>>(customers);
                return Ok(customersToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        [HttpGet]
        [Route("get-by-id/{customerId}")]
        public async Task<IActionResult> GetCustomerById(int? customerId)
        {
            try
            {
                if(customerId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var customer = await _customerService.GetCustomerByID(customerId.Value);
                var customerToSend = _mapper.Map<GetCustomer>(customer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        [HttpGet]
        [Route("get-by-name/{customerName}")]
        public async Task<IActionResult> GetByName(string customerName)
        {
            try
            {
                if (customerName == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var customer = await _customerService.GetCustomerByName(customerName);
                var customerToSend = _mapper.Map<GetCustomer>(customer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("get-customers-by-name/{namePattern}")]
        public async Task<IActionResult> GetCustomersByName(string namePattern)
        {
            try
            {
                if (namePattern == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var customers = await _customerService.GetCustomersByName(namePattern);
                var customersToSend = _mapper.Map<IList<GetCustomer>>(customers);
                return Ok(customersToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] RegisterOrUpdateCustomer updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(updateCustomer);
            }
            try
            {
                ApplicationCustomer appCustomer = await _userManager.FindByNameAsync(updateCustomer.EmailID);
                var result = await _userManager.RemovePasswordAsync(appCustomer);
                if (!result.Succeeded)
                {
                    _logger.LogError("Old Password Removal Failed" + result.ToString());
                    return StatusCode(StatusCodes.Status500InternalServerError, "Password Updation Failed");
                }
                result = await _userManager.AddPasswordAsync(appCustomer, updateCustomer.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("New Password Update Failed" + result.ToString());
                    return StatusCode(StatusCodes.Status500InternalServerError, "Password Updation Failed");
                }
                var customer = _mapper.Map<Customer>(updateCustomer);
                var customerToBeUpdated = await _customerService.UpdateCustomer(customer);
                var customerToSend = _mapper.Map<GetCustomer>(customerToBeUpdated);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("delete/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int? customerId)
        {
            try
            {
                if (customerId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var deletedCustomer = await _customerService.DeleteCustomer(customerId.Value);
                ApplicationCustomer appCustomer = await _userManager.FindByNameAsync(deletedCustomer.EmailID);
                if(appCustomer != null)
                {
                    await _userManager.DeleteAsync(appCustomer);
                }
                var customerToSend = _mapper.Map<GetCustomer>(deletedCustomer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

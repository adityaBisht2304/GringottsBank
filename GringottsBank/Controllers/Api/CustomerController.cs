using AutoMapper;
using GringottsBank.Models;
using GringottsBank.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] RegisterOrUpdateCustomer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newCustomer);
            }
            try
            {
                var customer = _mapper.Map<Customer>(newCustomer);
                var customerToBeCreated = await _customerService.CreateCustomer(customer);
                var customerToSend = _mapper.Map<GetCustomer>(customerToBeCreated);
                return Ok(customerToSend);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllCustomers")]
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
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int? id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                var customer = await _customerService.GetCustomerByID(id);
                var customerToSend = _mapper.Map<GetCustomer>(customer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getCustomerByName/{name}")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return NotFound();
                }
                var customer = await _customerService.GetCustomerByName(name);
                var customerToSend = _mapper.Map<GetCustomer>(customer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getCustomersByName/{name}")]
        public async Task<IActionResult> GetCustomersByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return NotFound();
                }
                var customers = await _customerService.GetCustomersByName(name);
                var customersToSend = _mapper.Map<IList<GetCustomer>>(customers);
                return Ok(customersToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] RegisterOrUpdateCustomer updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(updateCustomer);
            }
            try
            {
                var customer = _mapper.Map<Customer>(updateCustomer);
                var customerToBeUpdated = await _customerService.UpdateCustomer(customer);
                var customerToSend = _mapper.Map<GetCustomer>(customerToBeUpdated);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var deletedCustomer = await _customerService.DeleteCustomer(id);
                var customerToSend = _mapper.Map<GetCustomer>(deletedCustomer);
                return Ok(customerToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

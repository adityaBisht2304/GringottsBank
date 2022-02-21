using AutoMapper;
using GringottsBank.Models;
using GringottsBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Controllers.Api
{
    [Authorize]
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterAccount newAccount)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(newAccount);
            }
            try
            {
                var account = _mapper.Map<Account>(newAccount);
                var accountToBeCreated = await _accountService.CreateAccount(account);
                var accountToBeSend = _mapper.Map<GetAccount>(accountToBeCreated);
                return Ok(accountToBeSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAccounts([FromQuery] int? customerId)
        {
            try
            {
                if (customerId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var accounts = await _accountService.GetAllAccounts(customerId.Value);
                var accountsToSend = _mapper.Map<IList<GetAccount>>(accounts);
                return Ok(accountsToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetAccountById(int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var account = await _accountService.GetAccountByID(id.Value);
                var accountToSend = _mapper.Map<GetAccount>(account);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-by-account-number/{accountNumber}")]
        public async Task<IActionResult> GetByAccountNumber(long? accountNumber)
        {
            try
            {
                if (accountNumber == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var account = await _accountService.GetAccountByNumber(accountNumber.Value);
                var accountToSend = _mapper.Map<GetAccount>(account);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var deletedAccount = await _accountService.DeleteAccount(id.Value);
                var accountToSend = _mapper.Map<GetAccount>(deletedAccount);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

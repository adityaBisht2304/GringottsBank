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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("createAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterAccount newAccount)
        {
            if (!ModelState.IsValid)
            {
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
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllAccounts")]
        public async Task<IActionResult> GetAllAccounts([FromQuery] int? customerId)
        {
            try
            {
                if (customerId == null)
                {
                    return NotFound();
                }
                var accounts = await _accountService.GetAllAccounts(customerId.Value);
                var accountsToSend = _mapper.Map<IList<GetAccount>>(accounts);
                return Ok(accountsToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAccountById/{id}")]
        public async Task<IActionResult> GetAccountById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var account = await _accountService.GetAccountByID(id.Value);
                var accountToSend = _mapper.Map<GetAccount>(account);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAccountByNumber/{number}")]
        public async Task<IActionResult> GetAccountByNumber(long? number)
        {
            try
            {
                if (number == null)
                {
                    return NotFound();
                }
                var account = await _accountService.GetAccountByNumber(number.Value);
                var accountToSend = _mapper.Map<GetAccount>(account);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var deletedAccount = await _accountService.DeleteAccount(id.Value);
                var accountToSend = _mapper.Map<GetAccount>(deletedAccount);
                return Ok(accountToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

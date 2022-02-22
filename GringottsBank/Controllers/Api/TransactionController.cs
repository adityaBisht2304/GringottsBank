using AutoMapper;
using GringottsBank.Authentication;
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
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService,
            IMapper mapper,
            ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("deposit-money")]
        public async Task<IActionResult> DepositMoney([FromBody] NewTransaction newTransaction)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(newTransaction);
            }
            try
            {
                var transaction = _mapper.Map<Transaction>(newTransaction);
                var transactionToBeCreated = await _transactionService.DepositMoney(transaction);
                var transactionToBeSend = _mapper.Map<GetTransaction>(transactionToBeCreated);
                return Ok(transactionToBeSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("withdraw-money")]
        public async Task<IActionResult> WithdrawMoney([FromBody] NewTransaction newTransaction)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Value provided is not valid" + ModelState.ToString());
                return BadRequest(newTransaction);
            }
            try
            {
                var transaction = _mapper.Map<Transaction>(newTransaction);
                var transactionToBeCreated = await _transactionService.WithdrawMoney(transaction);
                var transactionToBeSend = _mapper.Map<GetTransaction>(transactionToBeCreated);
                return Ok(transactionToBeSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-all-transactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] int? accountId)
        {
            try
            {
                if (accountId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var transactions = await _transactionService.GetAllTransactions(accountId.Value);
                var transactionsToSend = _mapper.Map<IList<GetTransaction>>(transactions);
                return Ok(transactionsToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-account-transactions-in-time-period")]
        public async Task<IActionResult> GetAccountTransactionsInTimePeriod([FromQuery] int? accountId, [FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
        {
            try
            {
                if (accountId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }
                var transactions = await _transactionService.GetAccountTransactionsInTimePeriod(accountId.Value, fromTime, toTime);
                var transactionsToSend = _mapper.Map<IList<GetTransaction>>(transactions);
                return Ok(transactionsToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-customer-transactions-in-time-period")]
        public async Task<IActionResult> GetCustomerTransactionsInTimePeriod([FromQuery] int? customerId, [FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
        {
            try
            {
                if (customerId == null)
                {
                    _logger.LogError("Value provided is null");
                    return NotFound();
                }

                var transactions = await _transactionService.GetCustomerTransactionsInTimePeriod(customerId.Value, fromTime, toTime);
                var transactionsToSend = _mapper.Map<IList<GetTransaction>>(transactions);
                return Ok(transactionsToSend);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("depositMoney")]
        public async Task<IActionResult> DepositMoney([FromBody] NewTransaction newTransaction)
        {
            if (!ModelState.IsValid)
            {
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
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("withdrawMoney")]
        public async Task<IActionResult> WithdrawMoney([FromBody] NewTransaction newTransaction)
        {
            if (!ModelState.IsValid)
            {
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
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllTransactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] int? accountId)
        {
            try
            {
                if (accountId == null)
                {
                    return NotFound();
                }
                var transactions = await _transactionService.GetAllTransactions(accountId.Value);
                var transactionsToSend = _mapper.Map<IList<GetTransaction>>(transactions);
                return Ok(transactionsToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getTransactionsInTimePeriod")]
        public async Task<IActionResult> GetTransactionsInTimePeriod([FromQuery] int? accountId, [FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
        {
            try
            {
                if (accountId == null)
                {
                    return NotFound();
                }
                var transactions = await _transactionService.GetTransactionsInTimePeriod(accountId.Value, fromTime, toTime);
                var transactionsToSend = _mapper.Map<IList<GetTransaction>>(transactions);
                return Ok(transactionsToSend);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using GringottsBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public interface ITransactionService
    {
        Task<Transaction> WithdrawMoney(Transaction transaction);

        Task<Transaction> DepositMoney(Transaction transaction);

        Task<IEnumerable<Transaction>> GetAllTransactions(int accountID);

        Task<IEnumerable<Transaction>> GetTransactionsInTimePeriod(int accountID, DateTime fromTime, DateTime toTime);
    }
}
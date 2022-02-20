using GringottsBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public interface ITransactionService
    {
        Task<Transaction> WithdrawMoney(int amount);
        Task<Transaction> DepositMoney(int amount);
        Task<IEnumerable<Transaction>> GetAllTransactions(Account account);
        Task<IEnumerable<Transaction>> GetAllTransactions(Account account, DateTime fromTime, DateTime toTime);
    }
}
using GringottsBank.Data;
using GringottsBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly CustomerContext _dbContext;

        public TransactionService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction> DepositMoney(Transaction transaction)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == transaction.AccountID);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + transaction.AccountID + " does not exist");
            }

            account.AccountBalance += transaction.TransactionAmount;
            transaction.TransactionType = TransactionType.DEPOSIT;
            transaction.TransactionDateTime = DateTime.Now;
            transaction.TransactionStatus = TransactionStatus.SUCCESS;

            _dbContext.Accounts.Update(account);
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> WithdrawMoney(Transaction transaction)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == transaction.AccountID);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + transaction.AccountID + " does not exist");
            }

            if(transaction.TransactionAmount > account.AccountBalance)
            {
                throw new ApplicationException("Withdrawl Amount is more than the account balance");
            }

            account.AccountBalance -= transaction.TransactionAmount;
            transaction.TransactionType = TransactionType.WITHDRAW;
            transaction.TransactionDateTime = DateTime.Now;
            transaction.TransactionStatus = TransactionStatus.SUCCESS;

            _dbContext.Accounts.Update(account);
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int accountID)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == accountID);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + accountID + " does not exist");
            }

            var transactions = await _dbContext.Transactions.Where(t => t.AccountID == accountID).ToListAsync();
            if (transactions == null || transactions.Count == 0)
            {
                throw new ApplicationException("Transactions do not exist for account with ID:" + accountID);
            }
            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsInTimePeriod(int accountID, DateTime fromTime, DateTime toTime)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == accountID);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + accountID + " does not exist");
            }
            if(fromTime.Ticks > toTime.Ticks){
                throw new ApplicationException("From Time can not be greater than To Time");
            }
            var transactions = await _dbContext.Transactions.Where(t => t.AccountID == accountID).Where(t => t.TransactionDateTime >= fromTime).Where(t => t.TransactionDateTime <= toTime).ToListAsync();
            if (transactions == null || transactions.Count == 0)
            {
                throw new ApplicationException("Transactions do not exist for account with ID:" + accountID);
            }
            return transactions;
        }
    }
}

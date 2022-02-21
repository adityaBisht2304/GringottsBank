using GringottsBank.Data;
using GringottsBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly CustomerContext _dbContext;

        public AccountService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.ID == account.CustomerID);
            if (customer == null)
            {
                throw new ApplicationException("Customer with ID:" + account.CustomerID + " does not exist");
            }

            while (true)
            {
                long accountNumber = GenerateLongRandomNumber();
                var accountToBeCreated = _dbContext.Accounts.FirstOrDefault(c => c.AccountNumber.Equals(account.AccountNumber));
                if (accountToBeCreated == null)
                {
                    account.AccountNumber = accountNumber;
                    account.CreationDateTime = DateTime.Now;
                    break;
                }
            }

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return account;
        }

        public long GenerateLongRandomNumber()
        {
            Random random = new Random();
            byte[] bytes = new byte[8];
            random.NextBytes(bytes);
            bytes[7] = 0;
            bytes[6] = 63;
            return BitConverter.ToInt64(bytes, 0);
        }

        public async Task<Account> DeleteAccount(int id)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == id);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + id + " does not exist");
            }
            var transactions = await _dbContext.Transactions.Where(x => x.AccountID == account.ID).ToListAsync();
            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    _dbContext.Transactions.Remove(transaction);
                }
            }

            _dbContext.Accounts.Remove(account);                        
            await _dbContext.SaveChangesAsync();

            return account;
        }

        public async Task<Account> GetAccountByID(int id)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == id);
            if (account == null)
            {
                throw new ApplicationException("Account with ID:" + id + " does not exist");
            }
            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts(int customerID)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.ID == customerID);
            if (customer == null)
            {
                throw new ApplicationException("Customer with ID:" + customerID + " does not exist");
            }

            var accounts = await _dbContext.Accounts.Where(x => x.CustomerID == customerID).ToListAsync();
            if(accounts == null || accounts.Count == 0)
            {
                throw new ApplicationException("Accounts do not exist for customer with ID:"  + customerID);
            }
            return accounts;
        }

        public async Task<Account> GetAccountByNumber(long accountNumber)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new ApplicationException("Account with Number:" + accountNumber + " does not exist");
            }
            return account;
        }

        public Task<IEnumerable<Account>> GetAccountsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}

using GringottsBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(Account account);

        Task<IEnumerable<Account>> GetAllAccounts(int customerID);

        Task<Account> GetAccountByID(int id);

        Task<Account> GetAccountByNumber(long accountNumber);

        Task<IEnumerable<Account>> GetAccountsByName(string name);

        Task<Account> UpdateAccount(Account account);

        Task<Account> DeleteAccount(int id);
    }
}

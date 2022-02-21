using GringottsBank.Data;
using GringottsBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _dbContext;

        public CustomerService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var customerToBeCreated = await _dbContext.Customers.FirstOrDefaultAsync(c => c.EmailID.Equals(customer.EmailID));
            if(customerToBeCreated != null)
            {
                throw new ApplicationException("Email ID already exist");
            }
            //customer.AccountsID = new List<int>();
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.ID == id);
            if (customer == null)
            {
                throw new ApplicationException("Customer with ID:" + id + " does not exist");
            }
            var accounts = await _dbContext.Accounts.Where(x => x.CustomerID == customer.ID).ToListAsync();
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    var transactions = await _dbContext.Transactions.Where(x => x.AccountID == account.ID).ToListAsync();
                    if (transactions != null)
                    {
                        foreach (var transaction in transactions)
                        {
                            _dbContext.Transactions.Remove(transaction);
                        }
                    }
                    _dbContext.Accounts.Remove(account);
                }
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            if (customers == null)
            {
                throw new ApplicationException("No Customers Exist");
            }
            return customers;
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.ID == id);
            if (customer == null)
            {
                throw new ApplicationException("Customer with ID:" + id + " does not exist");
            }
            return customer;
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Name.Equals(name));
            if (customer == null)
            {
                throw new ApplicationException("Customer with Name:" + name + " does not exist");
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByName(string name)
        {
            var allUsers = await _dbContext.Customers.ToListAsync();
            var customers = allUsers.FindAll(c => c.Name.ToLower().Contains(name.ToLower()));
            if (customers == null)
            {
                throw new ApplicationException("Customers with Name:" + name + " does not exist");
            }
            return customers;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var customerToBeUpdated = await _dbContext.Customers.FirstOrDefaultAsync(c => c.EmailID.Equals(customer.EmailID));
            if (customerToBeUpdated == null)
            {
                throw new ApplicationException("Customers with EmailID:" + customer.EmailID + " does not exist");
            }
            customerToBeUpdated.Name = customer.Name;
            customerToBeUpdated.Password = customer.Password;

            _dbContext.Customers.Update(customerToBeUpdated);
            await _dbContext.SaveChangesAsync();
            return customerToBeUpdated;
        }
    }
}

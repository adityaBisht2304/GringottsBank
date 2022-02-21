using GringottsBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<Customer> GetCustomerByID(int id);

        Task<IEnumerable<Customer>> GetCustomersByName(string name);

        Task<Customer> GetCustomerByName(string name);

        Task<Customer> UpdateCustomer(Customer customer);

        Task<Customer> DeleteCustomer(int id);
    }
}

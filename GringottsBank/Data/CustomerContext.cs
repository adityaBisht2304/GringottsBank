using Microsoft.EntityFrameworkCore;
using GringottsBank.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GringottsBank.Authentication
{
    public class CustomerContext : IdentityDbContext<ApplicationCustomer>
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

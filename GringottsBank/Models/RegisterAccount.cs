using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class RegisterAccount
    {
        public AccountType AccountType { get; set; }
        public int CustomerID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class RegisterAccount
    {
        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        public int CustomerID { get; set; }
    }
}

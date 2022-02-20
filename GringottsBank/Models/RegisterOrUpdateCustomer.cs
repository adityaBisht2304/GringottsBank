using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class RegisterOrUpdateCustomer
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string EmailID { get; set; }

        [StringLength(128, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}

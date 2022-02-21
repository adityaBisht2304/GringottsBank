using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class RegisterOrUpdateCustomer
    {
        [EmailAddress]
        [Required]
        public string EmailID { get; set; }

        [StringLength(128, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

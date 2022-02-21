using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string EmailID { get; set; }

        [StringLength(128, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        public Customer()
        {

        }
    }
}

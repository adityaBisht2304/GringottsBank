using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public enum AccountType
    {
        SAVINGS,
        CURRENT
    }

    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int ID { get; set; }     
        
        public long AccountNumber { get; set; }

        public AccountType AccountType { get; set; }

        public int AccountBalance { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public Account()
        {
            
        }
    }
}

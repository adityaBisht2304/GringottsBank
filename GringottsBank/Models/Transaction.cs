using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public enum TransactionType
    {
        WITHDRAW,
        DEPOSIT
    }

    public enum TransactionStatus
    {
        SUCCESS,
        PENDING,
        FAILED
    }

    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int ID { get; set; }

        public string TransactionUniqueReference { get; set; }

        [Required]
        public int TransactionAmount { get; set; }

        public TransactionType TransactionType { get; set; }

        public TransactionStatus TransactionStatus { get; set; }
        
        public DateTime TransactionDateTime { get; set; }

        [Required]
        public int AccountID { get; set; }

        public Transaction()
        {
            TransactionUniqueReference = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32);
        }
    }
}

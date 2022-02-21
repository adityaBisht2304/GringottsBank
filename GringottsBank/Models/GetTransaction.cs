using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class GetTransaction
    {
        public int ID { get; set; }

        public string TransactionUniqueReference { get; set; }

        public int TransactionAmount { get; set; }

        public TransactionType TransactionType { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime TransactionDateTime { get; set; }

        public int AccountID { get; set; }
    }
}

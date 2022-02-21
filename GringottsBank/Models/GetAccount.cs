using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class GetAccount
    {
        public int ID { get; set; }

        public long AccountNumber { get; set; }

        public AccountType AccountType { get; set; }

        public float AccountBalance { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreationDateTime { get; set; }

        public int CustomerID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Models
{
    public class GetCustomer
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string EmailID { get; set; }
    }
}

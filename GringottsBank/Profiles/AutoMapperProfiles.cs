using AutoMapper;
using GringottsBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterOrUpdateCustomer, Customer>();
            CreateMap<Customer, GetCustomer>();
            CreateMap<RegisterAccount, Account>();
            CreateMap<Account, GetAccount>();
            CreateMap<NewTransaction, Transaction>();
            CreateMap<Transaction, GetTransaction>();
        }
    }
}

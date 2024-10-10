using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer? GetByName(string name);
        Customer? GetByEmail(string email);
    }
}

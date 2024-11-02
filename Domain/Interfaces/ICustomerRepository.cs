using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer? Authenticate(string username, string password);
    }
}

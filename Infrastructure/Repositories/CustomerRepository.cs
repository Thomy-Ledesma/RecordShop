using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Customer? Authenticate(string username, string password)
        {
            return _context.Customers.FirstOrDefault(c => c.Username == username && c.Password == password);
        }

    }
}   

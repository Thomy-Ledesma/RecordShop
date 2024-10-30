using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public Customer? GetByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public Customer? GetByName(string name)
        {
            return _context.Customers.FirstOrDefault(c => c.Username == name);
        } //considerar agregar a genericos por si lo queremos usar en albumes o admins
        public Customer? Authenticate(string username, string password)
        {
            Customer? userToAuthenticate = _context.Customers.FirstOrDefault(u => u.Username == username && u.Password == password);
            return userToAuthenticate;

        }

    }
}

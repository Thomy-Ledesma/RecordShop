using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository : EfRepository<Admin>, IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Admin? Authenticate(string username, string password)
        {
            return _context.Admins.FirstOrDefault(c => c.Username == username && c.Password == password);
        }

    }
}

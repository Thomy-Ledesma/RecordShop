using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SalesRepository : EfRepository<Sale>, ISalesRepository
    {
        private readonly ApplicationContext _context;
        public SalesRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

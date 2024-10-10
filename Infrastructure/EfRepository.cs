using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EfRepository<T> : RepositoryBase<T > where T : class
    {
        protected readonly ApplicationContext _context;
        public EfRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

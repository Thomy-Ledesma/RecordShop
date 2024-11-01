using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Sale>> ListWithProductsAsync()
        {
            return await _context.Sales
                .Include(s => s.SaleAlbums)
                    .ThenInclude(sa => sa.Album)
                .ToListAsync();
        }   
    }
}

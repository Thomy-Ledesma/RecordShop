using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ISalesService
    {
        Task<Sale> CreateSale(int customerId);
        Task<List<Sale>> GetAllSales();
        Task<Sale?> GetSaleWithProductsByIdAsync(int saleId);
        Task<string> AddAlbumToSale(int saleId, int albumId, int quantity);

        Task<string> CloseSale(int saleId);
    }
}

using Domain.Entities;

namespace Application.Services
{
    public interface ISalesService
    {
        Task<Sale> CreateSale(int customerId);
        Task<List<Sale>> GetAllSales();
        Task<Sale?> GetInProcessSale(int customerId);
        Task<Sale?> GetSaleWithProductsByIdAsync(int saleId);
        Task<string> AddAlbumToSale(int saleId, int albumId, int quantity);
        Task<string> RemoveAlbumFromSale(int saleId, int albumId, int quantity);
        Task<string> CloseSale(int customerId);

        Task<string> DeleteSale(int saleId);
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISalesRepository : IRepositoryBase<Sale>
    {
        Task<Sale?> GetInProcessSaleByCustomerIdAsync(int customerId);
        Task<Sale?> GetSaleWithProductsByIdAsync(int saleId);
        Task<List<Sale>> ListWithProductsAsync();

    }
}

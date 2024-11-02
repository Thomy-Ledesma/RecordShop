using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IAlbumRepository _albumRepository;

        public SalesService(ISalesRepository salesRepository, IAlbumRepository albumRepository)
        {
            _salesRepository = salesRepository;
            _albumRepository = albumRepository;
        }

        public async Task<List<Sale>> GetAllSales()
        {
            return await _salesRepository.ListWithProductsAsync();
        }

        public async Task<Sale?> GetSaleWithProductsByIdAsync(int saleId)
        {
            return await _salesRepository.GetSaleWithProductsByIdAsync(saleId);
        }

        public async Task<Sale?> GetInProcessSale(int customerId)
        {
            var existingSale = await _salesRepository.GetInProcessSaleByCustomerIdAsync(customerId);
            return existingSale;
        }

        public async Task<Sale?> CreateSale(int customerId)
        {
            var existingSale = await _salesRepository.GetInProcessSaleByCustomerIdAsync(customerId);

            if (existingSale != null)
            {
                return null;
            }

            var sale = new Sale
            {
                CustomerId = customerId,
                Total = 0
            };

            await _salesRepository.AddAsync(sale);

            return sale;
        }

        public async Task<string> AddAlbumToSale(int saleId, int albumId, int quantity = 1)
        {
            var sale = await _salesRepository.GetSaleWithProductsByIdAsync(saleId);

            if (sale == null)
            {
                return "Sale not found.";
            }

            if (sale.SaleState == State.Done)
            {
                return "Cannot modify a completed sale.";
            }

            var album = await _albumRepository.GetByIdAsync(albumId);
            if (album == null)
            {
                return "Album not found.";
            }

            var saleAlbum = sale.SaleAlbums.FirstOrDefault(sa => sa.AlbumId == albumId);

            if (saleAlbum != null)
            {
                saleAlbum.Quantity += quantity;
            }
            else
            {
                sale.SaleAlbums.Add(new SaleAlbum
                {
                    SaleId = saleId,
                    AlbumId = albumId,
                    Quantity = quantity
                });
            }

            sale.Total += album.Price * quantity;

            await _salesRepository.UpdateAsync(sale);

            return "Album added to sale successfully.";
        }

        public async Task<string> CloseSale(int customerId)
        {
            // Fetch the in-process sale for the specified customer
            var sale = await _salesRepository.GetInProcessSaleByCustomerIdAsync(customerId);

            if (sale == null)
            {
                return "No in-process sale found for this customer.";
            }

            // Check inventory for each album in the sale
            foreach (var saleAlbum in sale.SaleAlbums)
            {
                var album = saleAlbum.Album;

                if (album.Amount < saleAlbum.Quantity)
                {
                    return $"Insufficient stock for album '{album.Name}'. Available: {album.Amount}, Required: {saleAlbum.Quantity}.";
                }
            }

            // If stock is sufficient, update album quantities
            foreach (var saleAlbum in sale.SaleAlbums)
            {
                saleAlbum.Album.Amount -= saleAlbum.Quantity;
            }

            // Change sale status to Done
            sale.SaleState = State.Done;
            await _salesRepository.UpdateAsync(sale);

            return "Sale successfully closed.";
        }

        // New method to remove an album (or quantity) from an in-process sale
        public async Task<string> RemoveAlbumFromSale(int saleId, int albumId, int quantity)
        {
            var sale = await _salesRepository.GetSaleWithProductsByIdAsync(saleId);

            if (sale == null)
            {
                return "Sale not found.";
            }

            if (sale.SaleState == State.Done)
            {
                return "Cannot modify a completed sale.";
            }

            var saleAlbum = sale.SaleAlbums.FirstOrDefault(sa => sa.AlbumId == albumId);

            if (saleAlbum == null)
            {
                return "Album not found in sale.";
            }

            if (saleAlbum.Quantity < quantity)
            {
                return "Cannot remove more than the existing quantity.";
            }

            saleAlbum.Quantity -= quantity;

            if (saleAlbum.Quantity == 0)
            {
                sale.SaleAlbums.Remove(saleAlbum);
            }

            sale.Total -= saleAlbum.Album.Price * quantity;

            await _salesRepository.UpdateAsync(sale);

            return "Album removed from sale successfully.";
        }

        public async Task<string> DeleteSale(int saleId)
        {
            var sale = await _salesRepository.GetSaleWithProductsByIdAsync(saleId);

            if (sale == null)
            {
                return "Sale not found.";
            }

            if (sale.SaleState == State.Done)
            {
                return "Cannot cancel a completed sale.";
            }

            await _salesRepository.DeleteAsync(sale);

            return "Sale successfully cancelled.";
        }
    }
}

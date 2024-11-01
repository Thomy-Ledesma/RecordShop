using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Sale?> CreateSale(int customerId)
        {
            // Check for existing sale in "InProcess" state for this customer
            var existingSale = await _salesRepository.GetInProcessSaleByCustomerIdAsync(customerId);

            if (existingSale != null)
            {
                return null; // Indicates that a sale already exists
            }

            // Create a new sale
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
            

            // Check if the album is already in the sale
            var saleAlbum = sale.SaleAlbums.FirstOrDefault(sa => sa.AlbumId == albumId);

            if (saleAlbum != null)
            {
                // Album already exists in the sale; increment the quantity
                saleAlbum.Quantity += quantity;
            }
            else
            {
                // Add new album to the sale
                sale.SaleAlbums.Add(new SaleAlbum
                {
                    SaleId = saleId,
                    AlbumId = albumId,
                    Quantity = quantity
                });
            }

            // Update the total price of the sale
            var AlbumToAdd = await _albumRepository.GetByIdAsync(albumId);
            sale.Total += AlbumToAdd.Price * quantity;

            await _salesRepository.UpdateAsync(sale);

            return "Album added to sale successfully.";
        }

        public async Task<string> CloseSale(int saleId)
        {
            var sale = await _salesRepository.GetSaleWithProductsByIdAsync(saleId);

            if (sale == null)
            {
                return "Sale not found";
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
    }
    
}

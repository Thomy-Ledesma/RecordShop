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

        public async Task<Sale?> GetSaleById(int id)
        {
            return await _salesRepository.GetByIdAsync(id);
        }

        public async Task<Sale> CreateSale(int customerId)
        {
            var sale = new Sale
            {
                CustomerId = customerId,
                Total = 0,
            };

            return await _salesRepository.AddAsync(sale); // Persist the new sale
        }

        public async Task<Sale> AddAlbumToSale(int saleId, int albumId, int quantity = 1)
        {
            var sale = await _salesRepository.GetByIdAsync(saleId);
            if (sale == null) throw new Exception("Sale not found");

            var album = await _albumRepository.GetByIdAsync(albumId);
            if (album == null) throw new Exception("Album not found");

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
            return sale;
        }
    }
    
}

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

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<Sale>> GetAllSales()
        {
            return await _salesRepository.ListAsync(); // Generic method from EfRepository
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
            };
            return await _salesRepository.AddAsync(sale);
        }

        public async Task<Sale?> AddAlbum(Album album, int id)
        { 
            var saleToUpdate = await _salesRepository.GetByIdAsync(id);

            if (saleToUpdate == null)
            { 
                return null; //ver que hacer en caso de exepción
            }

            saleToUpdate.Products.Add(album);

            await _salesRepository.UpdateAsync(saleToUpdate);

            return saleToUpdate;
        }
    }
    
}

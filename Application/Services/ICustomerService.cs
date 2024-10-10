using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer> AddCustomer(AddCustomerRequest dto);
        Task UpdateCustomer(int id, AddCustomerRequest request);
        Task DeleteCustomer(Customer customer);
    }
}

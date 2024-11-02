using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer> AddCustomer(AddCustomerRequest dto);
        Task UpdateCustomer(int id, AddCustomerRequest request);
        Task DeleteCustomer(Customer customer);
        Customer Authenticate(CredentialsRequest credentials);
    }
}

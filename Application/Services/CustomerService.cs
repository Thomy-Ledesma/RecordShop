using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.ListAsync(); // Generic method from EfRepository
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer?> AddCustomer(AddCustomerRequest dto)
        {
            var existingCustomer = await _customerRepository.ListAsync();
            if (existingCustomer.Any(c => c.Username == dto.Username || c.Email == dto.Email))
            {
                return null;
            }

            var customer = new Customer
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
            };

            return await _customerRepository.AddAsync(customer);
        }
        public async Task UpdateCustomer(int id, AddCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            customer.Username = request.Username;
            customer.Password = request.Password;

            await _customerRepository.UpdateAsync(customer);

        }
        public async Task DeleteCustomer(Customer customer)
        {

            await _customerRepository.DeleteAsync(customer);

        }
        public Customer Authenticate(CredentialsRequest credentials)
        {
        var customer = _customerRepository.Authenticate(credentials.Username, credentials.Password);
        if (customer != null)
            {
                // You may now access the customer's role here
                return new Customer
                {
                    Id = customer.Id,
                    Role = customer.Role // Ensure this is included
                };
            }
            return null;
        }
    
    }
}

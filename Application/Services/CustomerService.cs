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

        public async Task<Customer> AddCustomer(AddCustomerRequest dto)
        {
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
            customer.Email = request.Email;

            await _customerRepository.UpdateAsync(customer);

        }
        public async Task DeleteCustomer(Customer customer)
        {

            await _customerRepository.DeleteAsync(customer);

        }
        public Customer Authenticate(CredentialsRequest credentials)
        {
           return _customerRepository.Authenticate(credentials.Username, credentials.Password);
        }

    }
}

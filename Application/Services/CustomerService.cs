using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            // Crear una nueva instancia de PasswordHasher
            var passwordHasher = new PasswordHasher<Customer>();

            var customer = new Customer
            {
                Email = dto.Email,
                Username = dto.Username,
                // Hashear la contraseña antes de guardarla
                Password = passwordHasher.HashPassword(null, dto.Password) // Asumimos que 'null' representa el objeto 'Customer'
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
            var customer = _customerRepository.(credentials.Username);
            if (customer == null)
            {
                return null; // Usuario no encontrado
            }

            var passwordHasher = new PasswordHasher<Customer>();
            var result = passwordHasher.VerifyHashedPassword(customer, customer.Password, credentials.Password);

            // Verifica si la contraseña es correcta
            return result == PasswordVerificationResult.Success ? customer : null;
        }

    }
}

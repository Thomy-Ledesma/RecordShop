using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }


        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            return await _adminRepository.ListAsync(); // Generic method from EfRepository
        }

        public async Task<Admin?> GetAdminById(int id)
        {
            return await _adminRepository.GetByIdAsync(id);
        }

        public async Task<Admin> AddAdmin(AddAdminRequest dto)
        {
            var admin = new Admin
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
            };
            return await _adminRepository.AddAsync(admin);
        }
        public async Task UpdateAdmin(int id, AddAdminRequest request)
        {
            var admin = await _adminRepository.GetByIdAsync(id);

            admin.Username = request.Username;
            admin.Password = request.Password;
            admin.Email = request.Email;

            await _adminRepository.UpdateAsync(admin);

        }
        public async Task DeleteAdmin(Admin admin)
        {

            await _adminRepository.DeleteAsync(admin);

        }
        public Admin Authenticate(CredentialsRequest credentials)
        {
            return _adminRepository.Authenticate(credentials.Username, credentials.Password);
        }

    }
}
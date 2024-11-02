using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

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

        public async Task<Admin?> AddAdmin(AddAdminRequest dto)
        {
            var existingAdmin = await _adminRepository.ListAsync();
            if (existingAdmin.Any(c => c.Username == dto.Username || c.Email == dto.Email))
            {
                return null;
            }

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

            await _adminRepository.UpdateAsync(admin);

        }
        public async Task DeleteAdmin(Admin admin)
        {

            await _adminRepository.DeleteAsync(admin);

        }
        public Admin Authenticate(CredentialsRequest credentials)
        {
            var admin = _adminRepository.Authenticate(credentials.Username, credentials.Password);
            if (admin != null)
            {
                // You may now access the customer's role here
                return new Admin
                {
                    Id = admin.Id,
                    Role = admin.Role // Ensure this is included
                };
            }
            return null;
        }

    }
}
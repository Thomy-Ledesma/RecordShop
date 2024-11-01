using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin?> GetAdminById(int id);
        Task<Admin> AddAdmin(AddAdminRequest dto);
        Task UpdateAdmin(int id, AddAdminRequest request);
        Task DeleteAdmin(Admin customer);
        Admin Authenticate(CredentialsRequest credentials);
    }
}

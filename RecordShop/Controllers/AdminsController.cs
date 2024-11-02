using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] despues
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var admins = await _adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var admin = await _adminService.GetAdminById(Id);
            if (admin == null)
            {
                return NotFound("Admin not found");
            }
            return Ok(admin);
        }

        [HttpPost("AddAdmin")]

        public async Task<IActionResult> AddAdmin(AddAdminRequest request)
        {
            var admin = await _adminService.AddAdmin(request);
            return Ok(admin);
        }

        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AddAdminRequest request)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound("Admin not found");
            }

            await _adminService.UpdateAdmin(id, request);

            return Ok("Customer updated");
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound("Customer not found");
            }

            await _adminService.DeleteAdmin(admin);

            return Ok("Admin deleted");
        }
    }

}

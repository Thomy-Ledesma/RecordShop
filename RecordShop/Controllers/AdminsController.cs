using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("GetById/{Id}")]

        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var admin = await _adminService.GetAdminById(Id);
            if (admin == null)
            {
                return NotFound("Admin not found");
            }
            return Ok(admin);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCustomer([FromBody] AddAdminRequest request)
        {
            var admin = await _adminService.AddAdmin(request);

            if (admin == null)
            {
                return BadRequest("Username or email is already in use.");
            }

            return Ok(admin);
        }

        [HttpPut("Update")]
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAdmin(int id)
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

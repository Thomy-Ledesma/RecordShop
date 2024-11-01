using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;

        public AuthenticationController(ICustomerService customerService, IAdminService adminService, IConfiguration configuration)
        {
            _customerService = customerService;
            _adminService = adminService;
            _config = configuration;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] CredentialsRequest credentialsRequest)
        {
            // Attempt authentication for Customer
            Customer? customer = _customerService.Authenticate(credentialsRequest);
            if (customer != null)
            {
                var token = GenerateJwtToken(customer.Id, UserRole.Customer);
                return Ok(new { token });
            }

            // Attempt authentication for Admin
            Admin? admin = _adminService.Authenticate(credentialsRequest);
            if (admin != null)
            {
                var token = GenerateJwtToken(admin.Id, UserRole.Admin);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(int userId, UserRole role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
    {
        new Claim("sub", userId.ToString()),
        new Claim("role", ((int)role).ToString()) // Store role as an integer
    };

            var jwtToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
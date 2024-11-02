using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] despues
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("GetById/{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customer = await _customerService.GetCustomerById(Id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest request)
        {
            var customer = await _customerService.AddCustomer(request);

            if (customer == null)
            {
                return BadRequest("Username or email is already in use.");
            }

            return Ok(customer);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomer(int  id, [FromBody] AddCustomerRequest request)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            await _customerService.UpdateCustomer(id, request);

            return Ok("Customer updated");
        }

        [HttpDelete("Delete")]
        [Authorize ]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            await _customerService.DeleteCustomer(customer);

            return Ok("Customer deleted");
        }
    } 

}

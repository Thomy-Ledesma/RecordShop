using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordShopCustomers : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public RecordShopCustomers(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customer = await _customerService.GetCustomerById(Id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
       
        public async Task<IActionResult> AddCustomer(AddCustomerRequest request)
        {
            var customer = await _customerService.AddCustomer(request);
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer")]
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

        [HttpDelete("DeleteCustomer")]
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

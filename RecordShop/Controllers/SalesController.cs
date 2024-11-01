using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "AdminOnly")] // Only admins can add customers
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // Endpoint to create a sale for a specific customer
        [HttpPost("OpenSale")]
        public async Task<IActionResult> OpenSale(int customerId)
        {
            var sale = await _salesService.CreateSale(customerId);

            if (sale == null)
            {
                return BadRequest("An open sale already exists for this customer.");
            }

            return Ok(new { message = "Sale created!", saleId = sale.Id });
        }

        // Endpoint to add an album to a sale with quantity
        [HttpPut("AddToCart")]
        public async Task<IActionResult> AddToCart(int saleId, int albumId, int quantity)
        {
            var result = await _salesService.AddAlbumToSale(saleId, albumId, quantity);

            if (result == "Sale not found.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("GetSaleById/{saleId}")]
        public async Task<IActionResult> GetSaleById(int saleId)
        {
            var sale = await _salesService.GetSaleWithProductsByIdAsync(saleId);
            if (sale == null)
            {
                return NotFound("Sale not found");
            }

            var result = new
            {
                sale.Id,
                sale.CustomerId,
                sale.Total,
                sale.SaleState,
                Albums = sale.SaleAlbums.Select(sa => new
                {
                    sa.Album.Id,
                    sa.Album.Name,
                    sa.Album.Price,
                    sa.Quantity
                })
            };

            return Ok(result);
        }

        [HttpPut("CloseSale")]
        public async Task<IActionResult> CloseSale(int saleId)
        {
            var result = await _salesService.CloseSale(saleId);

            if (result.StartsWith("Insufficient stock"))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // Endpoint to get all sales with albums and their quantities
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _salesService.GetAllSales();
            var result = sales.Select(sale => new
            {
                sale.Id,
                sale.CustomerId,
                sale.Total,
                sale.SaleState,
                Albums = sale.SaleAlbums.Select(sa => new
                {
                    sa.Album.Id,
                    sa.Album.Name,
                    sa.Album.Price,
                    sa.Quantity
                })
            });

            return Ok(result);
        }
    }
}
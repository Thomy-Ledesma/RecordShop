using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }


        [HttpGet("GetAllSales")]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("GetById/{saleId}")]
        [Authorize(Roles = "Admin")]
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

        [HttpPost("OpenSale")]
        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(int saleId, int albumId, int quantity)
        {
            var result = await _salesService.AddAlbumToSale(saleId, albumId, quantity);

            if (result == "Album added to sale successfully.")
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("GetCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var sale = await _salesService.GetInProcessSale(userId);
            if (sale == null)
            {
                return NotFound("This user doesn't currently have an open sale");
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
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CloseSale(int customerId)
        {
            var result = await _salesService.CloseSale(customerId);

            if (result.StartsWith("Insufficient stock") || result == "No in-process sale found for this customer.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("RemoveFromCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveFromCart(int saleId, int albumId, int quantity)
        {
            var sale = await _salesService.GetSaleWithProductsByIdAsync(saleId);
            if (sale == null)
            {
                return NotFound("Sale not found.");
            }

            if (sale.SaleState == State.Done)
            {
                return BadRequest("Cannot modify a completed sale.");
            }

            var result = await _salesService.RemoveAlbumFromSale(saleId, albumId, quantity);

            if (result.StartsWith("Album not found"))
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Endpoint to cancel an in-process sale
        [HttpPut("CancelSale")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelSale(int saleId)
        {
            var sale = await _salesService.GetSaleWithProductsByIdAsync(saleId);
            if (sale == null)
            {
                return NotFound("Sale not found.");
            }

            if (sale.SaleState == State.Done)
            {
                return BadRequest("Cannot cancel a completed sale.");
            }

            var result = await _salesService.DeleteSale(saleId);
            return Ok(result);
        }
    }
}
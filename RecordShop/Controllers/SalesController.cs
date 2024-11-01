using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly IAlbumService _albumService;

        public SalesController(ISalesService salesService, IAlbumService albumService)
        {
            _salesService = salesService;
            _albumService = albumService;
        }

        // Endpoint to create a sale for a specific customer
        [HttpPost("OpenSale")]
        public async Task<IActionResult> OpenSale(int customerId)
        {
            var sale = await _salesService.CreateSale(customerId);
            return Ok(new { message = "Sale created!", saleId = sale.Id });
        }

        // Endpoint to add an album to a sale with quantity
        [HttpPut("AddToCart")]
        public async Task<IActionResult> AddToCart(int saleId, int albumId, int quantity)
        {
            var album = await _albumService.GetAlbumById(albumId);
            if (album == null)
                return NotFound("Album not found");

            await _salesService.AddAlbumToSale(saleId, albumId, quantity);
            return Ok("Album added to sale with quantity updated.");
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
        [HttpPut("ClosePurchase/{saleId}")]

        public async Task<IActionResult> ClosePurchase(int saleId)
        {
            try
            {
                // Intenta cerrar la compra
                var sale = await _salesService.ClosePurchase(saleId);

                // Preparar la respuesta
                var response = new
                {
                    Sale = sale,
                    Message = "Purchase closed successfully, and album amounts updated."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Imprimir el mensaje de error en la consola para depuración
                Console.WriteLine($"Error: {ex.Message}");

                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
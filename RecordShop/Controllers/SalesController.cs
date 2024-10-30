using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly IAlbumService _albumService;

        public SalesController(ISalesService salesService, ICustomerService customerService, IAlbumService albumService)
        {
            _salesService = salesService;
            _customerService = customerService;
            _albumService = albumService;
        }

        [HttpPost("OpenSale")]

        public IActionResult OpenSale(int userId)
        {
            _salesService.CreateSale(userId);
            return Ok("sale created!");
        }

        [HttpGet("GetAllSales")]

        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _salesService.GetAllSales();
            return Ok(sales);
        }

        [HttpPut("AddToCart")]

        public async Task<IActionResult> AddToCart(int saleId, int albumId)
        {
            var album = await _albumService.GetAlbumById(albumId);
            if (album == null)
            {
                return NotFound("Album not found");
            }

            await _salesService.AddAlbum(album, saleId);

            return Ok("Album updated");
        }
    }
}

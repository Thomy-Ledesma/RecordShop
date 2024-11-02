using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAlbums()
        {
            var albums = await _albumService.GetAllAlbumsAsync();
            return Ok(albums);
        }

        [HttpGet("GetByID")]

        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var album = await _albumService.GetAlbumById(Id);
            if (album == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(album);
        }

        [HttpPost("AddAlbum")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddAlbum(AddAlbumRequest request)
        {
            var customer = await _albumService.AddAlbum(request);
            return Ok(customer);
        }

        [HttpPut("UpdateAlbum")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAlbum(int id, [FromBody] AddAlbumRequest request)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound("Album not found");
            }

            await _albumService.UpdateAlbum(id, request);

            return Ok("Album updated");
        }

        [HttpDelete("DeleteAlbum")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound("Customer not found");
            }

            await _albumService.DeleteAlbum(album);

            return Ok("Album deleted");
        }
    }
}

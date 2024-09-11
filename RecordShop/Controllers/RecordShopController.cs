 using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;

namespace RecordShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordShopController : ControllerBase
    {

        private readonly IAlbumService _albumService;

        public RecordShopController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        [HttpGet]
        public ActionResult<List<Album>> Get()
        {
            var Albums = _albumService.GetAllAlbums();
            return Ok(Albums);
        }

        [HttpPost]

        public IActionResult Add([FromBody]Album album)
        {
            return Ok(_albumService.AddAlbum(album));
        }
    }
}

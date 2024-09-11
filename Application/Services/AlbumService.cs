using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AlbumService : IAlbumService
    {   //inyeccion de dependencia
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public List<Album> GetAllAlbums()
        {
            var albums = _albumRepository.GetAllAlbums();
            return albums;
        }

        public string AddAlbum(Album album)
        {
            var album1 = new Album()
            {
                Id = album.Id,
                Name = album.Name,
                Band = album.Band,
                Genre = album.Genre,
                Amount = album.Amount,
                Price = album.Price,

            };
            
            return _albumRepository.AddAlbum(album1);


        }
    }
}

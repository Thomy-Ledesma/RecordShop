using Application.Repositories;
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
    }
}

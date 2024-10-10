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
        public List<Album> GetAll()
        {
            var albums = _albumRepository.GetAll();
            return albums;
        }
        
        public Album Get(string name)
        {
            return _albumRepository.Get(name);
        }

        /*public string Add(Album album)
        {
            return _albumRepository.Add(album);
        }*/
    }
}

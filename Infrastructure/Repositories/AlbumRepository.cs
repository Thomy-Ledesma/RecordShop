using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AlbumRepository : IAlbumRepository
    {

        private readonly ApplicationContext _context;
        public AlbumRepository(ApplicationContext context)
        {
            _context = context;
        }


        public List<Album> GetAllAlbums()
        {
            return _context.Albums
                .ToList();
        }

        public string AddAlbum(Album album)
        {
            _context.Albums.Add(album);
           
            return album.Name;
        }
    }
}

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


        public static List<Album> albums = new List<Album>() {

        new Album {Id = 1, Name = "mimimi", Band = "avril lavinge", Genre = "pop", Amount = 7, Price = 23}
        };

    public List<Album> GetAllAlbums()
        {
            return _context.Albums
                .ToList();
        }
    }
}

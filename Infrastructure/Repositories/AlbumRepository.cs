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

        public Album Get(string name)
        {
            return _context.Albums.First(a => a.Name == name);
        }
        public List<Album> GetAll()
        {
            return _context.Albums.ToList();
        }
    }
}

using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AlbumRepository : IAlbumRepository
    {
        public static List<Album> albums = new List<Album>() {

        new Album {Id = 1, Name = "mimimi", Band = "avril lavinge", Genre = "pop", Amount = 7, Price = 23}
        };

    public List<Album> GetAllAlbums()
        {
            return albums;
        }
    }
}

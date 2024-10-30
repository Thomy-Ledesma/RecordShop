using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using System.Diagnostics;
using System.Xml.Linq;

namespace Application.Services
{
    public class AlbumService : IAlbumService 
    { 
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _albumRepository.ListAsync();
        }

        public async Task<Album?> GetAlbumById(int id)
        {
            return await _albumRepository.GetByIdAsync(id);
        }
        public async Task<Album> AddAlbum(AddAlbumRequest dto)
        {
            var album = new Album
            {
                Name = dto.Name,
                Band = dto.band,
                Genre = dto.Genre,
                Amount = dto.Amount,
                Price   = dto.Price,
            };
            return await _albumRepository.AddAsync(album);
        }

        public async Task<Album> UpdateAlbum(int id, AddAlbumRequest dto)
        {
            var album = await GetAlbumById(id);
            album.Name = dto.Name;
            album.Band = dto.band;
            album.Genre = dto.Genre;
            album.Amount = dto.Amount;
            album.Price = dto.Price;

            await _albumRepository.UpdateAsync(album);

            return(album);

        }

        public async Task<string> DeleteAlbum(Album album)
        {
            await _albumRepository.DeleteAsync(album);

            return "Album deleted";
        }
    }
}

using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{   //use case
    public interface IAlbumService
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumById(int id);
        Task<Album> AddAlbum(AddAlbumRequest dto);
        Task<Album> UpdateAlbum(int id, AddAlbumRequest request);
        Task<string> DeleteAlbum(Album album);
    }
}

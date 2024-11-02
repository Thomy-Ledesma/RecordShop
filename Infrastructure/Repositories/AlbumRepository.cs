using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure
{
    public class AlbumRepository : EfRepository<Album>, IAlbumRepository
    {
        private readonly ApplicationContext _context;
        public AlbumRepository(ApplicationContext context) : base(context)
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

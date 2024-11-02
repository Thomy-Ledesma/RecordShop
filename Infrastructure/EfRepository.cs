using Infrastructure.Repositories;

namespace Infrastructure
{
    public class EfRepository<T> : RepositoryBase<T > where T : class
    {
        protected readonly ApplicationContext _context;
        public EfRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

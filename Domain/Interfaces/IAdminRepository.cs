using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdminRepository : IRepositoryBase<Admin>
    {
        Admin? Authenticate(string username, string password);
    }

}

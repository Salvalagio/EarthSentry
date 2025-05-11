using EarthSentry.Domain.Entities.Roles;
using EarthSentry.Domain.Repositories.Base;

namespace EarthSentry.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string roleName);
    }
}

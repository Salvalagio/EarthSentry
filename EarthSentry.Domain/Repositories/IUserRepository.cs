using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories.Base;

namespace EarthSentry.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}

using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;

namespace EarthSentry.Data.Repositories
{
    public class UserRoleRepository(EarthSentryDbContext context) : GenericRepository<UserRole>(context), IUserRoleRepository
    {
    }
}

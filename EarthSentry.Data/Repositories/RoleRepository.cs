using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Roles;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EarthSentry.Data.Repositories
{
    public class RoleRepository(EarthSentryDbContext context) : GenericRepository<Role>(context), IRoleRepository
    {
        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.ToLower() == roleName.ToLower());
        }
    }

}

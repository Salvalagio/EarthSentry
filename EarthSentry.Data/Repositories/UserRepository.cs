using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EarthSentry.Data.Repositories
{
    public class UserRepository(EarthSentryDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }
    }

}

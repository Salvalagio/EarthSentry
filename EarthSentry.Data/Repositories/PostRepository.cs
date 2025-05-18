using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EarthSentry.Data.Repositories
{
    public class PostRepository(EarthSentryDbContext context) : GenericRepository<Post>(context), IPostRepository
    {
        public async Task<IEnumerable<Post>> GetAllWithVotesAsync(int pageNumber, int pageSize)
        {
            return await _context.Post
                .Include(p => p.Votes)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}

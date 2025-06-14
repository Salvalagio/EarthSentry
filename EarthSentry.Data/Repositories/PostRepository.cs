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
                .Include(p => p.User)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Post
                .Include(p => p.Votes)
                .Where(p => p.Votes.Any() && (p.CreatedAt >= startDate && p.CreatedAt <= endDate))
                .ToListAsync();
        }

        public async Task<Post?> GetByPostId(int postId) 
            => await _context.Post
                             .Include(p => p.Votes)
                             .Include(p => p.User)
                             .Include(p => p.Comments)
                             .FirstOrDefaultAsync(x => x.PostId == postId);
    }
}

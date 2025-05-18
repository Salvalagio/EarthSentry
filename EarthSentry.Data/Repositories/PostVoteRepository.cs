using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EarthSentry.Data.Repositories
{
    public class PostVoteRepository(EarthSentryDbContext context) : GenericRepository<PostVote>(context), IPostVoteRepository
    {
        public async Task<PostVote?> GetByUserAndPostAsync(int postId, int userId)
        {
            return await _context.PostVote
                .FirstOrDefaultAsync(v => v.PostId == postId && v.UserId == userId);
        }
    }
}

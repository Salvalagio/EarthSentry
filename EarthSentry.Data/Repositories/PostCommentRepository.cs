using EarthSentry.Data.Base;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EarthSentry.Data.Repositories
{
    public class PostCommentRepository(EarthSentryDbContext context) : GenericRepository<PostComment>(context), IPostCommentRepository
    {
        public async Task<List<PostComment>> GetByPostIdAsync(int postId)
        {
            return await _context.PostComment
                .Include(x => x.User)
                .Where(v => v.PostId == postId)
                .ToListAsync();
        }

        public async Task<PostComment> GetByIdAndUserIdAsync(int commentId, int userId)
        {
            return await _context.PostComment
                                 .FirstOrDefaultAsync(v => v.CommentId == commentId && v.UserId == userId);
        }
    }
}

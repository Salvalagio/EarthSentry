using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories.Base;

namespace EarthSentry.Domain.Repositories
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        Task<List<PostComment>> GetByPostIdAsync(int postId);
        Task<PostComment> GetByIdAndUserIdAsync(int commentId, int userId);
    }
}

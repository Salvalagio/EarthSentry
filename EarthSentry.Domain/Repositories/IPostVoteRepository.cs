using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories.Base;

namespace EarthSentry.Domain.Repositories
{
    public interface IPostVoteRepository : IGenericRepository<PostVote> 
    {
        Task<PostVote?> GetByUserAndPostAsync(int postId, int userId);
    }
}

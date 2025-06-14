using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories.Base;

namespace EarthSentry.Domain.Repositories
{
    public interface IPostRepository : IGenericRepository<Post> 
    {
        Task<IEnumerable<Post>> GetAllWithVotesAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Post>> GetAllPostsByDateAsync(DateTime startDate, DateTime endDate);
        Task<Post?> GetByPostId(int postId);
    }
}

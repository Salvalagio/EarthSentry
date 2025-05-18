using EarthSentry.Contracts.Contracts.Posts;

namespace EarthSentry.Contracts.Interfaces.Business
{
    public interface IPostBusiness
    {
        Task<bool> AddPostAsync(PostCreateDto dto, int userId);
        Task<bool> EditPostAsync(int postId, PostUpdateDto dto);
        Task<bool> DeletePostAsync(int postId);

        Task<IEnumerable<PostWithVotesDto>> GetAllPostsAsync(int pageNumber, int pageSize = 10);
        Task<IEnumerable<PostWithVotesDto>> GetAllPostsWithUserVoteAsync(int userId, int pageNumber, int pageSize = 10);

        Task<bool> AddVoteAsync(int postId, int userId, short vote);
        Task<bool> RemoveVoteAsync(int postId, int userId);
    }

}

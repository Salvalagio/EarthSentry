using EarthSentry.Contracts.Contracts.Posts;

namespace EarthSentry.Contracts.Interfaces.Business
{
    public interface IPostBusiness
    {
        Task<bool> AddPostAsync(PostCreateDto dto, int userId);
        Task<bool> EditPostAsync(int postId, PostUpdateDto dto);
        Task<bool> DeletePostAsync(int postId);

        Task<PostWithVotesDto> GetPostsByIdAndUserAsync(int pageNumber, long userId);
        Task<IEnumerable<PostWithVotesDto>> GetAllPostsWithUserVoteAsync(int userId, int pageNumber, int pageSize = 3);

        Task<bool> AddVoteAsync(int postId, int userId, short vote);
        Task<bool> RemoveVoteAsync(int postId, int userId);

        Task<IEnumerable<PostCommentDto>> GetCommentsByPostIdAsync(int postId, int userId);
        Task<bool> AddCommentAsync(int postId, int userId, string comment);
        Task<bool> RemoveCommentAsync(int commentId, int userId);
        Task<IEnumerable<AdminPostSummaryDto>> GetTopIssues(DateTime startDate, DateTime endDate);
    }

}

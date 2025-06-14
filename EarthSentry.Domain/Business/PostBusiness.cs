using EarthSentry.Contracts.Contracts.Posts;
using EarthSentry.Contracts.Interfaces.Business;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EarthSentry.Domain.Business
{
    public class PostBusiness(ILogger<PostBusiness> _logger,
                              IPostRepository _postRepository,
                              IPostVoteRepository _voteRepository,
                              IUserRepository _userRepository,
                              IPostCommentRepository _postCommentRepository) : IPostBusiness
    {
        public async Task<bool> AddPostAsync(PostCreateDto dto, int userId)
        {
            try
            {
                var user = _userRepository.GetByIdAsync(userId).Result;
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found.", userId);
                    return false;
                }

                var post = new Post
                {
                    UserId = userId,
                    User = user,
                    Description = dto.Description,
                    ImageUrl = dto.ImageUrl,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _postRepository.AddAsync(post);
                await _postRepository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding post.");
                return false;
            }
        }

        public async Task<bool> EditPostAsync(int postId, PostUpdateDto dto)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return false;

            post.Description = dto.Description ?? post.Description;
            post.ImageUrl = dto.ImageUrl ?? post.ImageUrl;

            _postRepository.Update(post);
            await _postRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return false;

            _postRepository.Delete(post);
            await _postRepository.SaveAsync();
            return true;
        }

        public async Task<PostWithVotesDto> GetPostsByIdAndUserAsync(int postId, long userId)
        {
            var posts = await _postRepository.GetByPostId(postId);

            if (posts == null)
                return new();

            return new PostWithVotesDto
            {
                PostId = posts.PostId,
                Description = posts.Description,
                ImageUrl = posts.ImageUrl,
                Latitude = posts.Latitude,
                Longitude = posts.Longitude,
                CreatedAt = posts.CreatedAt,
                UpVotes = posts.Votes.Where(x => x.Vote > 0).Sum(v => v.Vote),
                DownVotes = posts.Votes.Where(x => x.Vote < 0).Sum(v => v.Vote),
                UserId = posts.UserId,
                UserVote = posts.Votes.FirstOrDefault(v => v.UserId == userId)?.Vote ?? 0,
                Comments = posts.Comments.Count,
                Username = posts.User.Username,
                UserImageUrl = posts.User.ImageUrl
            };
        }

        public async Task<IEnumerable<PostWithVotesDto>> GetAllPostsWithUserVoteAsync(int userId, int pageNumber, int pageSize = 3)
        {
            var posts = await _postRepository.GetAllWithVotesAsync(pageNumber, pageSize);

            return posts.Select(p => new PostWithVotesDto
            {
                PostId = p.PostId,
                UserId = p.UserId,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                CreatedAt = p.CreatedAt,
                UpVotes = p.Votes.Where(x => x.Vote > 0).Sum(v => v.Vote),
                DownVotes = p.Votes.Where(x => x.Vote < 0).Sum(v => v.Vote),
                UserVote = p.Votes.FirstOrDefault(v => v.UserId == userId)?.Vote ?? 0,
                Comments = p.Comments.Count,
                Username = p.User.Username,
                UserImageUrl = p.User.ImageUrl
            });
        }

        public async Task<IEnumerable<PostCommentDto>> GetCommentsByPostIdAsync(int postId, int userId)
        {
            var existingPost = await _postCommentRepository.GetByPostIdAsync(postId);
            if (existingPost == null)
                return [];

            var commentListDto = existingPost.Select(comment => new PostCommentDto
            {
                CommentId = comment.User.UserId == userId ? comment.CommentId : 0,
                UserImageUrl = comment.User.ImageUrl,
                Username = comment.User.Username,
                CreateAt = comment.CreatedAt,
                IsFromOwner = comment.User.UserId == userId,
                Content = comment.Content,
            });
            
            return commentListDto;
        }

        public async Task<bool> AddCommentAsync(int postId, int userId, string comment)
        {
            var existingPost = await _postRepository.GetByIdAsync(postId);
            if (existingPost == null)
                return false;

            var postComment = new PostComment
            {
                PostId = postId,
                UserId = userId,
                Post = existingPost,
                User = await _userRepository.GetByIdAsync(userId),
                Content = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _postCommentRepository.AddAsync(postComment);
            await _postCommentRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveCommentAsync(int commentId, int userId)
        {
            var existingComment = await _postCommentRepository.GetByIdAndUserIdAsync(commentId, userId);
            if (existingComment == null)
                return false;


            _postCommentRepository.Delete(existingComment);
            await _postCommentRepository.SaveAsync();
            return true;
        }

        public async Task<bool> AddVoteAsync(int postId, int userId, short vote)
        {
            var existingVote = await _voteRepository.GetByUserAndPostAsync(postId, userId);
            if (existingVote != null)
                return false;

            var postVote = new PostVote
            {
                PostId = postId,
                UserId = userId,
                Vote = vote,
                Post = await _postRepository.GetByIdAsync(postId),
                User = await _userRepository.GetByIdAsync(userId),
                CreatedAt = DateTime.UtcNow
            };

            await _voteRepository.AddAsync(postVote);
            await _voteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveVoteAsync(int postId, int userId)
        {
            var vote = await _voteRepository.GetByUserAndPostAsync(postId, userId);
            if (vote == null) return false;

            _voteRepository.Delete(vote);
            await _voteRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<AdminPostSummaryDto>> GetTopIssues(DateTime startDate, DateTime endDate)
        {
            var posts = await _postRepository.GetAllPostsByDateAsync(startDate, endDate);

            var adminSummary =  posts.Select(p => new AdminPostSummaryDto
            {
                PostId = p.PostId,
                ImageUrl = p.ImageUrl,
                Title = p.Description.Length > 50 ? p.Description[..47] + "..." : p.Description,
                Category = "NoCategory",
                VoteCount = p.Votes.Count(),
            });

            return adminSummary.OrderByDescending(p => p.VoteCount).Take(10);
        }
    }

}

using EarthSentry.Contracts.Contracts.Posts;
using EarthSentry.Contracts.Interfaces.Business;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EarthSentry.Domain.Business
{
    public class PostBusiness(ILogger<PostBusiness> _logger,
                              IPostRepository _postRepo,
                              IPostVoteRepository _voteRepo) : IPostBusiness
    {
        public async Task<bool> AddPostAsync(PostCreateDto dto, int userId)
        {
            try
            {
                var post = new Post
                {
                    UserId = userId,
                    Description = dto.Description,
                    ImageUrl = dto.ImageUrl,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _postRepo.AddAsync(post);
                await _postRepo.SaveAsync();
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
            var post = await _postRepo.GetByIdAsync(postId);
            if (post == null) return false;

            post.Description = dto.Description ?? post.Description;
            post.ImageUrl = dto.ImageUrl ?? post.ImageUrl;

            _postRepo.Update(post);
            await _postRepo.SaveAsync();
            return true;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _postRepo.GetByIdAsync(postId);
            if (post == null) return false;

            _postRepo.Delete(post);
            await _postRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PostWithVotesDto>> GetAllPostsAsync(int pageNumber, int pageSize = 10)
        {
            var posts = await _postRepo.GetAllWithVotesAsync(pageNumber, pageSize);

            return posts.Select(p => new PostWithVotesDto
            {
                PostId = p.PostId,
                UserId = p.UserId,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                CreatedAt = p.CreatedAt,
                VoteCount = p.Votes.Sum(v => v.Vote)
            });
        }

        public async Task<IEnumerable<PostWithVotesDto>> GetAllPostsWithUserVoteAsync(int userId, int pageNumber, int pageSize = 10)
        {
            var posts = await _postRepo.GetAllWithVotesAsync(pageNumber, pageSize);

            return posts.Select(p => new PostWithVotesDto
            {
                PostId = p.PostId,
                UserId = p.UserId,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                CreatedAt = p.CreatedAt,
                VoteCount = p.Votes.Sum(v => v.Vote),
                UserVote = p.Votes.FirstOrDefault(v => v.UserId == userId)?.Vote ?? 0
            });
        }

        public async Task<bool> AddVoteAsync(int postId, int userId, short vote)
        {
            var existingVote = await _voteRepo.GetByUserAndPostAsync(postId, userId);
            if (existingVote != null)
                return false;

            var postVote = new PostVote
            {
                PostId = postId,
                UserId = userId,
                Vote = vote,
                CreatedAt = DateTime.UtcNow
            };

            await _voteRepo.AddAsync(postVote);
            await _voteRepo.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveVoteAsync(int postId, int userId)
        {
            var vote = await _voteRepo.GetByUserAndPostAsync(postId, userId);
            if (vote == null) return false;

            _voteRepo.Delete(vote);
            await _voteRepo.SaveAsync();
            return true;
        }
    }

}

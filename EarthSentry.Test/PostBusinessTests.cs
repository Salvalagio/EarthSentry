using EarthSentry.Contracts.Contracts.Posts;
using EarthSentry.Domain.Business;
using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EarthSentry.Test
{
    public class PostBusinessTests
    {
        private readonly Mock<ILogger<PostBusiness>> _loggerMock = new();
        private readonly Mock<IPostRepository> _postRepoMock = new();
        private readonly Mock<IPostVoteRepository> _voteRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IPostCommentRepository> _commentRepoMock = new();

        private PostBusiness CreatePostBusinessMocked() =>
            new(_loggerMock.Object, _postRepoMock.Object, _voteRepoMock.Object, _userRepoMock.Object, _commentRepoMock.Object);

        [Fact]
        public async Task AddPostAsync_UserNotFound_ReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddPostAsync(new PostCreateDto(), 1);

            Assert.False(result);
        }

        [Fact]
        public async Task AddPostAsync_ValidUser_AddsPostAndReturnsTrue()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new User { UserId = 1 });
            _postRepoMock.Setup(r => r.AddAsync(It.IsAny<Post>())).Returns(Task.CompletedTask);
            _postRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddPostAsync(new PostCreateDto { Description = "desc" }, 1);

            Assert.True(result);
            _postRepoMock.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
            _postRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task EditPostAsync_PostNotFound_ReturnsFalse()
        {
            _postRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.EditPostAsync(1, new PostUpdateDto());

            Assert.False(result);
        }

        [Fact]
        public async Task EditPostAsync_ValidPost_UpdatesAndReturnsTrue()
        {
            var post = new Post { PostId = 1, Description = "old", ImageUrl = "old.jpg" };
            _postRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(post);
            _postRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.EditPostAsync(1, new PostUpdateDto { Description = "new", ImageUrl = "new.jpg" });

            Assert.True(result);
            Assert.Equal("new", post.Description);
            Assert.Equal("new.jpg", post.ImageUrl);
            _postRepoMock.Verify(r => r.Update(post), Times.Once);
            _postRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePostAsync_PostNotFound_ReturnsFalse()
        {
            _postRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.DeletePostAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeletePostAsync_ValidPost_DeletesAndReturnsTrue()
        {
            var post = new Post { PostId = 1 };
            _postRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(post);
            _postRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.DeletePostAsync(1);

            Assert.True(result);
            _postRepoMock.Verify(r => r.Delete(post), Times.Once);
            _postRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPostsByIdAndUserAsync_PostNotFound_ReturnsEmptyDto()
        {
            _postRepoMock.Setup(r => r.GetByPostId(It.IsAny<int>())).ReturnsAsync((Post)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetPostsByIdAndUserAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(0, result.PostId);
        }

        [Fact]
        public async Task GetPostsByIdAndUserAsync_ValidPost_ReturnsDto()
        {
            var post = new Post
            {
                PostId = 1,
                UserId = 2,
                Description = "desc",
                ImageUrl = "img",
                Latitude = 1,
                Longitude = 2,
                CreatedAt = DateTime.UtcNow,
                Votes = new List<PostVote>
                {
                    new PostVote { UserId = 1, Vote = 1 },
                    new PostVote { UserId = 2, Vote = -1 }
                },
                Comments = new List<PostComment> { new PostComment(), new PostComment() },
                User = new User { Username = "user", ImageUrl = "img" }
            };
            _postRepoMock.Setup(r => r.GetByPostId(1)).ReturnsAsync(post);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetPostsByIdAndUserAsync(1, 1);

            Assert.Equal(1, result.PostId);
            Assert.Equal(1, result.UpVotes);
            Assert.Equal(-1, result.DownVotes);
            Assert.Equal(2, result.Comments);
            Assert.Equal("user", result.Username);
        }

        [Fact]
        public async Task GetAllPostsWithUserVoteAsync_ReturnsDtos()
        {
            var posts = new List<Post>
            {
                new Post
                {
                    PostId = 1,
                    UserId = 2,
                    Description = "desc",
                    ImageUrl = "img",
                    Latitude = 1,
                    Longitude = 2,
                    CreatedAt = DateTime.UtcNow,
                    Votes = new List<PostVote>
                    {
                        new PostVote { UserId = 1, Vote = 1 },
                        new PostVote { UserId = 2, Vote = -1 }
                    },
                    Comments = new List<PostComment> { new PostComment() },
                    User = new User { Username = "user", ImageUrl = "img" }
                }
            };
            _postRepoMock.Setup(r => r.GetAllWithVotesAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(posts);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetAllPostsWithUserVoteAsync(1, 1);

            Assert.Single(result);
            Assert.Equal(1, result.First().UpVotes);
            Assert.Equal(-1, result.First().DownVotes);
        }

        [Fact]
        public async Task GetCommentsByPostIdAsync_PostNotFound_ReturnsEmpty()
        {
            _commentRepoMock.Setup(r => r.GetByPostIdAsync(It.IsAny<int>())).ReturnsAsync((List<PostComment>)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetCommentsByPostIdAsync(1, 1);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCommentsByPostIdAsync_ValidPost_ReturnsDtos()
        {
            var comments = new List<PostComment>
            {
                new PostComment
                {
                    CommentId = 1,
                    User = new User { UserId = 1, Username = "user", ImageUrl = "img" },
                    CreatedAt = DateTime.UtcNow,
                    Content = "test"
                },
                new PostComment
                {
                    CommentId = 2,
                    User = new User { UserId = 2, Username = "user2", ImageUrl = "img2" },
                    CreatedAt = DateTime.UtcNow,
                    Content = "test2"
                }
            };
            _commentRepoMock.Setup(r => r.GetByPostIdAsync(1)).ReturnsAsync(comments);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetCommentsByPostIdAsync(1, 1);

            Assert.Equal(2, result.Count());
            Assert.True(result.First().IsFromOwner);
            Assert.Equal(1, result.First().CommentId);
            Assert.Equal(0, result.Last().CommentId);
        }

        [Fact]
        public async Task AddCommentAsync_PostNotFound_ReturnsFalse()
        {
            _postRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddCommentAsync(1, 1, "comment");

            Assert.False(result);
        }

        [Fact]
        public async Task AddCommentAsync_ValidPost_AddsAndReturnsTrue()
        {
            _postRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Post());
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _commentRepoMock.Setup(r => r.AddAsync(It.IsAny<PostComment>())).Returns(Task.CompletedTask);
            _commentRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddCommentAsync(1, 1, "comment");

            Assert.True(result);
            _commentRepoMock.Verify(r => r.AddAsync(It.IsAny<PostComment>()), Times.Once);
            _commentRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveCommentAsync_CommentNotFound_ReturnsFalse()
        {
            _commentRepoMock.Setup(r => r.GetByIdAndUserIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((PostComment)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.RemoveCommentAsync(1, 1);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveCommentAsync_ValidComment_DeletesAndReturnsTrue()
        {
            var comment = new PostComment { CommentId = 1 };
            _commentRepoMock.Setup(r => r.GetByIdAndUserIdAsync(1, 1)).ReturnsAsync(comment);
            _commentRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.RemoveCommentAsync(1, 1);

            Assert.True(result);
            _commentRepoMock.Verify(r => r.Delete(comment), Times.Once);
            _commentRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task AddVoteAsync_AlreadyVoted_ReturnsFalse()
        {
            _voteRepoMock.Setup(r => r.GetByUserAndPostAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PostVote());

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddVoteAsync(1, 1, 1);

            Assert.False(result);
        }

        [Fact]
        public async Task AddVoteAsync_NotVoted_AddsAndReturnsTrue()
        {
            _voteRepoMock.Setup(r => r.GetByUserAndPostAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((PostVote)null);
            _postRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Post());
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _voteRepoMock.Setup(r => r.AddAsync(It.IsAny<PostVote>())).Returns(Task.CompletedTask);
            _voteRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.AddVoteAsync(1, 1, 1);

            Assert.True(result);
            _voteRepoMock.Verify(r => r.AddAsync(It.IsAny<PostVote>()), Times.Once);
            _voteRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveVoteAsync_VoteNotFound_ReturnsFalse()
        {
            _voteRepoMock.Setup(r => r.GetByUserAndPostAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((PostVote)null);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.RemoveVoteAsync(1, 1);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveVoteAsync_ValidVote_DeletesAndReturnsTrue()
        {
            var vote = new PostVote { PostVoteId = 1 };
            _voteRepoMock.Setup(r => r.GetByUserAndPostAsync(1, 1)).ReturnsAsync(vote);
            _voteRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.RemoveVoteAsync(1, 1);

            Assert.True(result);
            _voteRepoMock.Verify(r => r.Delete(vote), Times.Once);
            _voteRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTopIssues_ReturnsOrderedTop10()
        {
            var posts = Enumerable.Range(1, 15).Select(i =>
                new Post
                {
                    PostId = i,
                    ImageUrl = $"img{i}",
                    Description = new string('a', i + 40),
                    Votes = Enumerable.Range(0, i).Select(_ => new PostVote()).ToList()
                }).ToList();

            _postRepoMock.Setup(r => r.GetAllPostsByDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(posts);

            var mockedPostBusiness = CreatePostBusinessMocked();
            var result = await mockedPostBusiness.GetTopIssues(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);

            Assert.Equal(10, result.Count());
            Assert.True(result.First().VoteCount >= result.Last().VoteCount);
            Assert.All(result, dto => Assert.True(dto.Title.Length <= 50));
        }
    }
}

using EarthSentry.Contracts.Contracts.Posts;
using EarthSentry.Contracts.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace EarthSentry.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController(IPostBusiness _postBusiness) : ControllerBase
    {
        #region Posts
        [HttpGet("details/{postId}")]
        public async Task<ActionResult<PostWithVotesDto>> GetAllPosts([FromRoute] int postId, [FromHeader] long userId)
        {
            var posts = await _postBusiness.GetPostsByIdAndUserAsync(postId, userId);
            return Ok(posts);
        }

        [HttpGet("feed/{userId}")]
        public async Task<ActionResult> GetAllPostsWithUserVotes([FromRoute] int userId, [FromQuery] int page)
        {
            var posts = await _postBusiness.GetAllPostsWithUserVoteAsync(userId, page);
            return Ok(new { posts, HasMore = posts?.Any() ?? false });
        }

        [HttpPost("create/{userId}")]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDto dto, int userId)
        {
            var result = await _postBusiness.AddPostAsync(dto, userId);
            if (!result)
                return BadRequest(new { message = "Could not create post." });

            return Ok();
        }

        [HttpPut("edit/{postId}")]
        public async Task<IActionResult> EditPost(int postId, [FromBody] PostUpdateDto dto)
        {
            var result = await _postBusiness.EditPostAsync(postId, dto);
            if (!result)
                return NotFound(new { message = "Post not found or could not be edited." });

            return Ok(new { message = "Post updated successfully." });
        }

        [HttpDelete("delete/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var result = await _postBusiness.DeletePostAsync(postId);
            if (!result)
                return NotFound(new { message = "Post not found or could not be deleted." });

            return Ok(new { message = "Post deleted successfully." });
        }
        #endregion

        #region Votes

        [HttpPost("vote")]
        public async Task<IActionResult> AddVote([FromBody] PostVoteDto dto)
        {
            var result = await _postBusiness.AddVoteAsync(dto.PostId, dto.UserId, dto.Vote);
            if (!result)
                return BadRequest(new { message = "Vote could not be registered." });

            return Ok(new { message = "Vote registered successfully." });
        }

        [HttpDelete("vote")]
        public async Task<IActionResult> RemoveVote([FromQuery] int postId, [FromHeader] int userId)
        {
            var result = await _postBusiness.RemoveVoteAsync(postId, userId);
            if (!result)
                return NotFound(new { message = "Vote not found or could not be removed." });

            return Ok(new { message = "Vote removed successfully." });
        }
        #endregion

        #region Comments
        [HttpGet("comments/{postId}")]
        public async Task<ActionResult<PostCommentDto>> GetAllCommentsFromPost([FromRoute] int postId, [FromHeader] int userId)
        {
            var comments = await _postBusiness.GetCommentsByPostIdAsync(postId, userId);
            return Ok(comments);
        }

        [HttpPost("comments")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequestoDto dto)
        {
            var result = await _postBusiness.AddCommentAsync(dto.PostId, dto.UserId, dto.CommentDescription);
            if (!result)
                return BadRequest(new { message = "Comment could not be registered." });

            return Ok(new { message = "Comment registered successfully." });
        }

        [HttpDelete("comments")]
        public async Task<IActionResult> RemoveComment([FromQuery] int commentId, [FromHeader] int userId)
        {
            var result = await _postBusiness.RemoveCommentAsync(commentId, userId);
            if (!result)
                return NotFound(new { message = "Comment not found or could not be removed." });

            return Ok(new { message = "Comment removed successfully." });
        }
        #endregion

        #region Post Admin
        [HttpGet("admin/issues")]
        public async Task<ActionResult<AdminPostSummaryDto>> GetTopIssues([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var topPosts = await _postBusiness.GetTopIssues(startDate, endDate);
            return Ok(topPosts);
        }
        #endregion
    }
}

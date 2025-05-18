using EarthSentry.Contracts.Contracts.Posts;
using EarthSentry.Contracts.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace EarthSentry.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController(IPostBusiness _postBusiness) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostWithVotesDto>>> GetAllPosts([FromQuery] int page)
        {
            var posts = await _postBusiness.GetAllPostsAsync(page);
            return Ok(posts);
        }

        [HttpGet("feed/{userId}")]
        public async Task<ActionResult<IEnumerable<PostWithVotesDto>>> GetAllPostsWithUserVotes([FromRoute] int userId, [FromQuery] int page)
        {
            var posts = await _postBusiness.GetAllPostsWithUserVoteAsync(userId, page);
            return Ok(posts);
        }

        [HttpPost("create/{userId}")]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDto dto, int userId)
        {
            var result = await _postBusiness.AddPostAsync(dto, userId);
            if (!result)
                return BadRequest(new { message = "Could not create post." });

            return Ok(new { message = "Post created successfully." });
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

        [HttpPost("vote")]
        public async Task<IActionResult> AddVote([FromBody] PostVoteDto dto)
        {
            var result = await _postBusiness.AddVoteAsync(dto.PostId, dto.UserId, dto.Vote);
            if (!result)
                return BadRequest(new { message = "Vote could not be registered." });

            return Ok(new { message = "Vote registered successfully." });
        }

        [HttpDelete("vote")]
        public async Task<IActionResult> RemoveVote([FromQuery] int postId, [FromQuery] int userId)
        {
            var result = await _postBusiness.RemoveVoteAsync(postId, userId);
            if (!result)
                return NotFound(new { message = "Vote not found or could not be removed." });

            return Ok(new { message = "Vote removed successfully." });
        }
    }
}

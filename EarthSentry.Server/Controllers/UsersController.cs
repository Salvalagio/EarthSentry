using EarthSentry.Contracts.Contracts.Users.Dtos;
using EarthSentry.Contracts.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace EarthSentry.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserBusiness _userBusiness) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userBusiness.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id, [FromHeader] string password)
        {
            var user = await _userBusiness.GetUserByIdAsync(id, password);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id, string password)
        {
            var result = await _userBusiness.DeleteUserAsync(id, password);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto loginDto)
        {
            var (Success, UserId, UserImage) = await _userBusiness.LoginUserAsync(loginDto);
            if (!Success)
                return Unauthorized();

            return Ok(new { userId = UserId, imageUrl = UserImage });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto registerDto)
        {
            var (Success, Message, UserId) = await _userBusiness.RegisterUserAsync(registerDto);
            if (!Success)
                return BadRequest(new { message = Message });

            return Ok(new { userId = UserId });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updateDto)
        {
            var result = await _userBusiness.UpdateUserAsync(updateDto);
            if (!result)
                return NotFound();

            return Ok(new { message = "User updated successfully" });
        }
    }
}

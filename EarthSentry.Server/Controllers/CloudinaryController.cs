using EarthSentry.Contracts.Interfaces.Services;
using EarthSentry.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace EarthSentry.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CloudinaryController(ICloudinaryService _cloudinaryService) : ControllerBase
    {
        [HttpGet("SignIn")]
        public async Task<ActionResult<IEnumerable<CloudinarySignInDto>>> GetCloudinaryInfo()
        {
            var signInDto = await _cloudinaryService.SignIn();
            return Ok(signInDto);
        }
    }
}

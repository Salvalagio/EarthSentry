using EarthSentry.Contracts.Services;

namespace EarthSentry.Contracts.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<CloudinarySignInDto> SignIn();
    }
}

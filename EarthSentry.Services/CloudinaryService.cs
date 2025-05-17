using CloudinaryDotNet;
using EarthSentry.Contracts.Interfaces.Services;
using EarthSentry.Contracts.Services;

namespace EarthSentry.Services
{
    public class CloudinaryService : Cloudinary, ICloudinaryService
    {
        public CloudinaryService() : base(Environment.GetEnvironmentVariable("CLOUDINARY_URL"))
        {
            this.Api.Secure = true;
            this.CreateFolder("earthsentry");
        }

        public async Task<CloudinarySignInDto> SignIn()
        {
            return await Task.Run(() =>
            {
                var timestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                var parameters = new SortedDictionary<string, object>
                {
                    { "timestamp", timestamp },
                    { "folder", "earthsentry" }
                };

                var signature = this.Api.SignParameters(parameters);

                return new CloudinarySignInDto
                {
                    Timestamp = timestamp,
                    Signature = signature,
                    ApiKey = 383451275764556,
                    CloudName = "EarthSentryImageRepo",
                    Folder = "earthsentry"
                };
            });
        }
    }
}

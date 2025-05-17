namespace EarthSentry.Contracts.Services
{
    public class CloudinarySignInDto
    {
        public long Timestamp { get; set; }
        public string Signature { get; set; }
        public long ApiKey { get; set; }
        public string CloudName { get; set; }
        public string Folder { get; set; }
    }

}

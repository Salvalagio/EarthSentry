namespace EarthSentry.Contracts.Contracts.Posts
{
    public class PostCreateDto
    {
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

}

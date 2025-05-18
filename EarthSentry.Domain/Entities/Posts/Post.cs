using EarthSentry.Domain.Entities.Users;

namespace EarthSentry.Domain.Entities.Posts
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public ICollection<PostVote> Votes { get; set; }
    }

}

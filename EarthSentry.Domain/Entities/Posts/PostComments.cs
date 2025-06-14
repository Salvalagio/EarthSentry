using EarthSentry.Domain.Entities.Users;

namespace EarthSentry.Domain.Entities.Posts
{
    public class PostComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}

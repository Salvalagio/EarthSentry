using EarthSentry.Domain.Entities.Posts;

namespace EarthSentry.Domain.Entities.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = [];

        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<PostVote> PostVotes { get; set; } = [];
    }
}

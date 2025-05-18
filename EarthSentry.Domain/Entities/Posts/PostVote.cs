using EarthSentry.Domain.Entities.Users;

namespace EarthSentry.Domain.Entities.Posts
{
    public class PostVote
    {
        public int PostVoteId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// Vote value. 1 for upvote, -1 for downvote.
        /// </summary>
        public short Vote { get; set; }
        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }

}

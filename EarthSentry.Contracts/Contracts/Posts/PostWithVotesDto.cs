namespace EarthSentry.Contracts.Contracts.Posts
{
    public class PostWithVotesDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public int VoteCount { get; set; }
        /// <summary>
        /// User vote for the post. -1 for downvote, 1 for upvote, 0 for no vote.
        /// </summary>
        public int UserVote { get; set; }
        public string Username { get; set; }
        public object UserImageUrl { get; set; }
    }
}

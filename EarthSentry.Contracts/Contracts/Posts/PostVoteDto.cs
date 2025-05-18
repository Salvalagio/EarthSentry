namespace EarthSentry.Contracts.Contracts.Posts
{
    public class PostVoteDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// Vote value. 1 for upvote, -1 for downvote.
        /// </summary>
        public short Vote { get; set; }
    }
}

namespace EarthSentry.Contracts.Contracts.Posts
{
    public class AdminPostSummaryDto
    {
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Future feature: Category of the post, e.g., "Wildlife", "Environment", etc. Also this could be an enum.
        /// </summary>
        public string Category { get; set; }
        public int VoteCount { get; set; }

    }
}

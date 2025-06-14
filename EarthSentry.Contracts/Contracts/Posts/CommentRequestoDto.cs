namespace EarthSentry.Contracts.Contracts.Posts
{
    public class CommentRequestoDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string CommentDescription { get; set; }
    }
}

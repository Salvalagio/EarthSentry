namespace EarthSentry.Contracts.Contracts.Posts
{
    public class PostCommentDto
    {
        public int CommentId { get; set; }
        public string UserImageUrl { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsFromOwner { get; set; }
    }
}

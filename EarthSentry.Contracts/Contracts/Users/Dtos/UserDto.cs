namespace EarthSentry.Contracts.Contracts.Users.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}

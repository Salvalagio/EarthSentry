namespace EarthSentry.Contracts.Contracts.Users.Dtos
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
    }
}

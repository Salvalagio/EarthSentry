using EarthSentry.Domain.Entities.Users;

namespace EarthSentry.Domain.Entities.Roles
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

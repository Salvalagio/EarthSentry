using EarthSentry.Domain.Entities.Roles;
using EarthSentry.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;

namespace EarthSentry.Data.Base
{
    public class EarthSentryDbContext(DbContextOptions<EarthSentryDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("earth");

            #region User
            modelBuilder.Entity<User>()
                .ToTable("tb_users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasColumnName("userid");

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnName("passwordhash")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnName("createdat")
                .HasConversion(MainDateTimeConverter)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.LastLogin)
                .HasConversion(MainDateTimeConverter)
                .HasColumnName("lastlogin");

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasColumnName("isactive")
                .HasDefaultValue(true);
            #endregion

            #region Role

            modelBuilder.Entity<Role>()
                .ToTable("tb_roles")
                .HasKey(r => r.RoleId);

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleId)
                .HasColumnName("roleid");

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasColumnName("rolename")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);
            #endregion

            #region UserRole
            modelBuilder.Entity<UserRole>()
                .ToTable("tb_userroles")
                .HasKey(ur => ur.UserRoleId);

            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.UserRoleId)
                .HasColumnName("userroleid");

            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.RoleId)
                .HasColumnName("roleid");

            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.UserId)
                .HasColumnName("userid");

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

        #region Utils
        private static ValueConverter<DateTime,DateTime> MainDateTimeConverter 
            =>  new(v => v.ToUniversalTime(),  
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        #endregion
    }
}

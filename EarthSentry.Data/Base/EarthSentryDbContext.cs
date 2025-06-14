using EarthSentry.Domain.Entities.Posts;
using EarthSentry.Domain.Entities.Roles;
using EarthSentry.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EarthSentry.Data.Base
{
    public class EarthSentryDbContext(DbContextOptions<EarthSentryDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostVote> PostVote { get; set; }
        public DbSet<PostComment> PostComment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("earth");

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("tb_users");
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId)
                      .HasColumnName("userid");
                entity.Property(u => u.Username)
                      .HasColumnName("username")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(false);
                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(100)
                      .IsRequired()
                      .IsUnicode(false);
                entity.Property(u => u.PasswordHash)
                      .HasColumnName("passwordhash")
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(u => u.CreatedAt)
                      .HasColumnName("createdat")
                      .HasConversion(MainDateTimeConverter)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(u => u.LastLogin)
                      .HasConversion(MainDateTimeConverter)
                      .HasColumnName("lastlogin");
                entity.Property(u => u.IsActive)
                      .HasColumnName("isactive")
                      .HasDefaultValue(true);
                entity.Property(u => u.ImageUrl)
                      .HasColumnName("imageurl");

                entity.HasMany(u => u.Posts)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Comments)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Role>(entity => {
                entity.ToTable("tb_roles");
                entity.HasKey(r => r.RoleId);
                entity.Property(r => r.RoleId)
                      .HasColumnName("roleid");
                entity.Property(r => r.RoleName)
                      .HasColumnName("rolename")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity => { 
            
                entity.ToTable("tb_userroles");
                entity.HasKey(ur => ur.UserRoleId);

                entity.Property(ur => ur.UserRoleId)
                      .HasColumnName("userroleid");

                entity.Property(ur => ur.RoleId)
                      .HasColumnName("roleid");

                entity.Property(entity => entity.UserId)
                      .HasColumnName("userid");

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("tb_posts", "earth");

                entity.HasKey(p => p.PostId);

                entity.Property(p => p.PostId)
                      .HasColumnName("postid");

                entity.Property(p => p.UserId)
                      .HasColumnName("userid")
                      .IsRequired();

                entity.Property(p => p.Description)
                      .HasColumnName("description")
                      .IsRequired();

                entity.Property(p => p.ImageUrl)
                      .HasColumnName("imageurl")
                      .IsRequired();

                entity.Property(p => p.Latitude)
                      .HasColumnName("latitude")
                      .HasColumnType("decimal(9,6)");

                entity.Property(p => p.Longitude)
                      .HasColumnName("longitude")
                      .HasColumnType("decimal(9,6)");

                entity.Property(p => p.CreatedAt)
                      .HasColumnName("createdat")
                      .HasConversion(MainDateTimeConverter)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(p => p.UpdatedAt)
                      .HasColumnName("updatedat")
                      .HasConversion(MainDateTimeConverter)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PostVote>(entity =>
            {
                entity.ToTable("tb_postvotes", "earth");

                entity.HasKey(v => v.PostVoteId);

                entity.Property(v => v.PostVoteId)
                      .HasColumnName("postvoteid");

                entity.Property(v => v.PostId)
                      .HasColumnName("postid")
                      .IsRequired();

                entity.Property(v => v.UserId)
                      .HasColumnName("userid")
                      .IsRequired();

                entity.Property(v => v.Vote)
                      .HasColumnName("vote")
                      .IsRequired();

                entity.Property(v => v.CreatedAt)
                      .HasColumnName("createdat")
                      .HasConversion(MainDateTimeConverter)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(v => v.Post)
                      .WithMany(p => p.Votes)
                      .HasForeignKey(v => v.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.User)
                      .WithMany(u => u.PostVotes)
                      .HasForeignKey(v => v.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(v => new { v.PostId, v.UserId })
                      .IsUnique();
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.ToTable("tb_comments", "earth");

                entity.HasKey(c => c.CommentId);

                entity.Property(c => c.CommentId)
                      .HasColumnName("commentid");

                entity.Property(c => c.PostId)
                      .HasColumnName("postid")
                      .IsRequired();

                entity.Property(c => c.UserId)
                      .HasColumnName("userid")
                      .IsRequired();

                entity.Property(c => c.Content)
                      .HasColumnName("content")
                      .IsRequired();

                entity.Property(c => c.CreatedAt)
                      .HasColumnName("createdat")
                      .HasConversion(MainDateTimeConverter)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        #region Utils
        private static ValueConverter<DateTime,DateTime> MainDateTimeConverter 
            =>  new(v => v.ToUniversalTime(),  
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        #endregion
    }
}

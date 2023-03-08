using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.Email).IsRequired();
                user.HasIndex(u => u.Email).IsUnique();
                user.HasMany(u => u.Posts)
                    .WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId);
            });

            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(e => e.Id);
                post.Property(e => e.Title).IsRequired().HasMaxLength(100);
                post.Property(e => e.Content).IsRequired();
            });
        }
    }
}
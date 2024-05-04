using Microsoft.EntityFrameworkCore;

namespace Blog_Management_System.Models
{
    public class BlogManagementSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public BlogManagementSystemDbContext(DbContextOptions<BlogManagementSystemDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Forum)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ForumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Forum>()
                .HasOne(f => f.User)
                .WithMany(u => u.Forums)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

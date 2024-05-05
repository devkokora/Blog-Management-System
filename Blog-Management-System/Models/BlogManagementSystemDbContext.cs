using Microsoft.EntityFrameworkCore;

namespace Blog_Management_System.Models;

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
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<Status>().HasData(
            new Status()
            {
                StatusId = 1,
                StatusName = "Hot",
            },
            new Status()
            {
                StatusId = 2,
                StatusName = "New",
            });

        modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                CategoryId = 1,
                CategoryName = "alien"
            },
            new Category()
            {
                CategoryId = 2,
                CategoryName = "ufo"
            },
            new Category()
            {
                CategoryId = 3,
                CategoryName = "dog"
            },
            new Category()
            {
                CategoryId = 4,
                CategoryName = "cat"
            },
            new Category()
            {
                CategoryId = 5,
                CategoryName = "nasa"
            },
            new Category()
            {
                CategoryId = 6,
                CategoryName = "zombie"
            });

        modelBuilder.Entity<Comment>().HasData(
            new Comment()
            {
                CommentId = 1,
                Body = "wtf..",
                ForumId = 1,
                Like = 2,
                UserId = 2,
            });

        modelBuilder.Entity<Forum>().HasData(
            new Forum()
            {
                ForumId = 1,
                Title = "I found alien last year",
                Body = "hello",
                Like = 3,
                Created_at = DateTime.Now,
                UserId = 1,
            },
            new Forum()
            {
                ForumId = 2,
                Title = "Hello from another world",
                Body = "...",
                Like = 18,
                Created_at = DateTime.Now,
                UserId = 2,
            });

        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Username = "chukka",
                Role = "admin",
                Created_at = DateTime.Now
            },
            new User()
            {
                Id = 2,
                Username = "otto",
                Role = "user",
                Created_at = DateTime.Now
            },
            new User()
            {
                Id = 3,
                Username = "Juijui",
                Role = "user",
                Created_at = DateTime.Now
            });
    }

}

using Blog_Management_System.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace Blog_Management_System.Models;

public class BlogManagementSystemDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public BlogManagementSystemDbContext(DbContextOptions<BlogManagementSystemDbContext> options) : base(options)
    {

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.EnableSensitiveDataLogging();
    //}
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

        modelBuilder.Entity<Category>().HasData(
             new Category()
             {
                 Id = 1,
                 Name = "Alien"
             },
             new Category()
             {
                 Id = 2,
                 Name = "UFO"
             },
             new Category()
             {
                 Id = 3,
                 Name = "Dog"
             },
             new Category()
             {
                 Id = 4,
                 Name = "Cat"
             },
             new Category()
             {
                 Id = 5,
                 Name = "Nasa"
             },
             new Category()
             {
                 Id = 6,
                 Name = "Zombie"
             });

        //modelBuilder.Entity<Comment>().HasData(
        //    new Comment()
        //    {
        //        CommentId = 1,
        //        Body = "wtf..",
        //        ForumId = 1,
        //        Like = 2,
        //        UserId = 2,
        //    });

        //modelBuilder.Entity<Forum>().HasData(
        //    new Forum()
        //    {
        //        ForumId = 1,
        //        Title = "I found alien last year",
        //        Body = "hello",
        //        Like = 3,
        //        Created_at = DateTime.Now,
        //        UserId = 1,
        //    },
        //    new Forum()
        //    {
        //        ForumId = 2,
        //        Title = "Hello from another world",
        //        Body = "...",
        //        Like = 18,
        //        Created_at = DateTime.Now,
        //        UserId = 2,
        //    });

        //modelBuilder.Entity<User>().HasData(
        //    new User()
        //    {
        //        Id = 1,
        //        Username = "chukka",
        //        Role = "admin",
        //        Created_at = DateTime.Now
        //    },
        //    new User()
        //    {
        //        Id = 2,
        //        Username = "otto",
        //        Role = "user",
        //        Created_at = DateTime.Now
        //    },
        //    new User()
        //    {
        //        Id = 3,
        //        Username = "Juijui",
        //        Role = "user",
        //        Created_at = DateTime.Now
        //    });
    }

}

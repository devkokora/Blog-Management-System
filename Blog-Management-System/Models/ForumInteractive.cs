using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
namespace Blog_Management_System.Models;

public class ForumInteractive : IForumInteractive
{
    private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;
    public User? User { get; set; }
    public List<Forum>? Forums { get; set; }

    public ForumInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
    {
        _blogManagementSystemDbContext = blogManagementSystemDbContext;
    }

    public void CreateForum(Forum forum)
    {
        _blogManagementSystemDbContext.Forums.Add(forum);
        _blogManagementSystemDbContext.SaveChanges();
    }

    public void EditForum(Forum forum)
    {
        if (User is not null && Forums is not null)
        {
            if (Forums.Any(f => f.ForumId == forum.ForumId)
            && (forum.UserId == User.Id || User.Role == "admin"))
            {
                _blogManagementSystemDbContext.Entry(forum).State = EntityState.Detached;
                _blogManagementSystemDbContext.Forums.Update(forum);
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
    }

    public List<Forum>? GetAllForums()
    {
        Forums = [.. _blogManagementSystemDbContext.Forums];
        return Forums;
    }

    public List<Forum>? GetForumsByDetails(List<Status>? statuses, List<Category>? categories)
    {
        var forums = new List<Forum>();

        if (Forums is null)
            return null;

        if (statuses is not null)
        {
            var temp = GetForumsBy(statuses);
            if (temp is not null)
                forums.AddRange(temp);
        }

        if (categories is not null)
        {
            var temp = GetForumsBy(categories);
            if (temp is not null)
                forums.AddRange(temp);
        }

        return [.. forums.Distinct()];
    }

    private List<Forum>? GetForumsBy<T>(List<T> filters) where T : class
    {
        var forums = new List<Forum>();
        if (filters is not null && Forums is not null)
        {
            forums.AddRange(Forums
                .Where(f => f.Categories is not null && f.Categories
                .Any(fc => filters
                .Any(c => c == fc))));
        }
        return forums;
    }

    public List<Forum>? GetForumsByUserId(int userId)
    {
        if (userId == 0)
            return null;
        return [.. Forums?.Where(f => f.UserId == userId)];
    }

    public void RemoveForum(int? id)
    {
        var forum = _blogManagementSystemDbContext.Forums.Find(id);
        if (User is not null && forum is not null)
        {
            if (User.Id == forum.UserId || User.Role == "admin")
            {
                _blogManagementSystemDbContext.Remove(forum);
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
    }
}

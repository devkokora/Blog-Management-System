
namespace Blog_Management_System.Models
{
    public class ForumInteractive : IForumInteractive
    {
        private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;
        public User? User {get; set;}
        public List<Forum>? Forums { get; set; }

        public ForumInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
        {
            _blogManagementSystemDbContext = blogManagementSystemDbContext;
        }

        public void CreateForum(Forum forum)
        {
            throw new NotImplementedException();
        }

        public void EditForum(Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<Forum>? GetAllForums()
        {
            throw new NotImplementedException();
        }

        public List<Forum>? GetForumsByDetails(List<Status> statuses, List<Category> categories)
        {
            throw new NotImplementedException();
        }

        public List<Forum>? GetForumsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveForum(Forum forum)
        {
            throw new NotImplementedException();
        }
    }
}

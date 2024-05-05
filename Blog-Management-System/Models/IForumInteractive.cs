namespace Blog_Management_System.Models
{
    public interface IForumInteractive
    {
        User? User { get; set; }
        List<Forum>? Forums { get; set; }
        void CreateForum(Forum forum);
        void EditForum(Forum forum);
        void RemoveForum(int? id);
        List<Forum>? GetAllForums();
        List<Forum>? GetForumsByDetails(List<Status> statuses, List<Category> categories);
        List<Forum>? GetForumsByUserId(int userId);
    }
}

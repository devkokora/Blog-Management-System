using Blog_Management_System.Models.Tags;

namespace Blog_Management_System.Models.Interactives;

public interface IForumInteractive
{
    User? User { get; set; }
    List<Forum>? Forums { get; set; }
    void CreateForum(Forum forum);
    void EditForum(Forum forum);
    void RemoveForum(int? id);
    List<Forum>? GetAllForums();
    List<Forum>? GetForumsByTags(List<Status> statuses, List<Category> categories);
    List<Forum>? GetForumsByUserId(int userId);
}

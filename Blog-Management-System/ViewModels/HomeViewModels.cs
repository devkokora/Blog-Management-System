using Blog_Management_System.Models;

namespace Blog_Management_System.ViewModels;

public class HomeViewModels
{
    public User? User { get; set; }
    public List<User>? Users { get; set; }
    public List<Forum>? Forums { get; }
    public Forum? Forum { get; set; } = new();
    public ForumMetaData MetaData { get; set; } = new();
    public HomeViewModels(List<Forum>? forums, List<User>? users, User? user, Forum? forum)
    {
        Forums = forums;
        User = user;
        Users = users;
        Forum = forum;
    }
}

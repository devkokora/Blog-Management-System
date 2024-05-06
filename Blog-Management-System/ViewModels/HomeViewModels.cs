using Blog_Management_System.Models;

namespace Blog_Management_System.ViewModels
{
    public class HomeViewModels
    {
        public User? User { get; set; }
        public List<Forum>? Forums { get; }
        public Forum Forum { get; set; } = new();
        public HomeViewModels(List<Forum>? forums, User? user)
        {
            Forums = forums;
            User = user;
        }
    }
}

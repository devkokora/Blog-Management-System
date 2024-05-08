namespace Blog_Management_System.Models.Interactives
{
    public interface IUserInteractive
    {
        public User? User { get; set; }
        User? GetUserByUserName(string? username);
        User? CreateUser(string? username);
        List<User>? GetAllUser();
    }
}

namespace Blog_Management_System.Models.Interactives
{
    public interface IUserInteractive
    {
        User? GetUserByUserName(string? username);
        User? CreateUser(string? username);
    }
}

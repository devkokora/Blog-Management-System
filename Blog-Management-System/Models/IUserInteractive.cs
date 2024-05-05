namespace Blog_Management_System.Models
{
    public interface IUserInteractive
    {
        User? GetUserByUserName(string username);
        User? CreateUser(string username);
    }
}

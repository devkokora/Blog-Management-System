namespace Blog_Management_System.Models;

public class ForumMetaData
{
    public List<CategoryType>? SelectedCategories { get; set; }
    public List<StatusType>? SelectedStatuses { get; set; }
}

public enum CategoryType
{
    Alien = 1,
    UFO,
    Dog,
    Cat,
    Nasa,
    Zombie
}

public enum StatusType
{
    Hot = 1,
    New
}

using Blog_Management_System.Models.Tags;

namespace Blog_Management_System.Models.Interactives;

public interface IStatusInteractive
{
    List<Forum>? HotStatus { get; set; }
    List<Forum>? NewStatus { get; set; }
    List<Status> Statuses { get; set; }
    void UpdateStatus(List<Forum>? forums);
}

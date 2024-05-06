using Blog_Management_System.Models.Tags;

namespace Blog_Management_System.Models.Interactives;

public class StatusInteractive : IStatusInteractive
{
    public List<Forum>? HotStatus { get; set; }
    public List<Forum>? NewStatus { get; set; }
    public List<Status> Statuses { get; set; }

    public StatusInteractive()
    {
        Statuses = [new Status
        {
            Id = 1,
            Name = "Hot",
            Forums = HotStatus
        },
        new Status
        {
            Id = 2,
            Name = "New",
            Forums = NewStatus
        }];
    }
    public void UpdateStatus(List<Forum>? forums)
    {
        HotStatus = [];
        NewStatus = [];        

        if (forums is not null)
        {
            HotStatus.AddRange(GetHotStatusForum(forums));
            NewStatus.AddRange(GetNewStatusForum(forums));
            Statuses[0].Forums = HotStatus;
            Statuses[1].Forums = NewStatus;
        }
    }
    private List<Forum> GetHotStatusForum(List<Forum> forums)
    {
        // Hot is top 10 calculate by the most like/day
        return forums.OrderByDescending(f => f.Like / CalculateHot(f.Created_at)).Take(10).ToList();
    }

    private int CalculateHot(DateTime created_at)
    {
        int calc = (DateTime.Now - created_at).Hours;
        return calc > 0 ? calc : 1;
    }

    private List<Forum> GetNewStatusForum(List<Forum> forums)
    {
        // New is all forum in a day.
        return [.. forums.Where(f => (DateTime.Now - f.Created_at).TotalDays <= 1)];
    }
}

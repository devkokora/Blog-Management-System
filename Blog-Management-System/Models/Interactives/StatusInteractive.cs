using System.Linq;

namespace Blog_Management_System.Models.Interactives
{
    public class StatusInteractive : IStatusInteractive
    {
        public List<Forum>? HotStatus { get; private set; }
        public List<Forum>? NewStatus { get; private set; }

        public void UpdateStatus(List<Forum> forums)
        {
            HotStatus = [];
            NewStatus = [];

            HotStatus.AddRange(GetHotStatusForum(forums));
            NewStatus.AddRange(GetNewStatusForum(forums));
        }
        private List<Forum> GetHotStatusForum(List<Forum> forums)
        {
            // Hot is top 10 calculate by the most like/day
            return [.. forums.OrderByDescending(f => f.Like / CalculateHot(f.Created_at)).Take(10)];
        }

        private int CalculateHot(DateTime created_at)
        {
            int calc = (DateTime.Now - created_at).Hours;
            return calc > 0 ? calc : 1;
        }

        private List<Forum> GetNewStatusForum(List<Forum> forums)
        {
            // New is all forum in a day.
            var temp = new List<Forum>();

            return [.. forums.Where(f => (DateTime.Now.Day -  f.Created_at.Day) < 1)];
        }
    }
}

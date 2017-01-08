namespace TaskManager.DataLayer.Common.Filters
{
    public class TasksByUserFilter
    {
        public TasksByUserFilter(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }
    }
}
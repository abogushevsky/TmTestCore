namespace TaskManager.DataLayer.Common.Filters
{
    public class TasksByCategoryFilter
    {
        public TasksByCategoryFilter(int categoryId)
        {
            this.CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}
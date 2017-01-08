namespace TaskManager.DataLayer.Common.Filters
{
    public class CategoriesByUserFilter
    {
        public CategoriesByUserFilter(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }
    }
}
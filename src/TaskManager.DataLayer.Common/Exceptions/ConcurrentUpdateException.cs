namespace TaskManager.DataLayer.Common.Exceptions
{
    public class ConcurrentUpdateException : RepositoryException
    {
        public const int ERROR_CODE = 50005;

        public ConcurrentUpdateException() : base()
        {
            
        }
    }
}
namespace TaskManager.DataLayer.Common.Exceptions
{
    public class ConcurrentUpdateException : RepositoryException
    {
        public const int ErrorCode = 50005;

        public ConcurrentUpdateException() : base()
        {
            
        }
    }
}
using System;
using TaskManager.DataLayer.Common.Properties;

namespace TaskManager.DataLayer.Common.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException() : base(Resources.RepositoryExText)
        {
            
        }

        public RepositoryException(string message) : base(message)
        {
            
        }

        public RepositoryException(Exception originalException) : base(Resources.RepositoryExText, originalException)
        {
            
        }

        public RepositoryException(string message, Exception originalException) : base(message, originalException)
        {

        }
    }
}
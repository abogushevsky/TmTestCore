using System;
using TaskManager.DataLayer.Common.Properties;

namespace TaskManager.DataLayer.Common.Exceptions
{
    public class EntityCreateException : RepositoryException
    {
        public EntityCreateException() : base(Resources.RepositoryCreateExText)
        {
            
        }

        public EntityCreateException(Exception originalException) : base(Resources.RepositoryExText, originalException)
        {

        }
    }
}
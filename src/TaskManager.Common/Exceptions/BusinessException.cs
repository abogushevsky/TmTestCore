using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Properties;

namespace TaskManager.Common.Exceptions
{
    /// <summary>
    /// Исключение уровня бизнес логики. Сообщения этого типа исключений могут быть показаны пользователю
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException() : base(Resources.CommonExText)
        {
            
        }

        public BusinessException(string message) : base(message)
        {
            Contract.Requires(message != null);
        }
    }
}
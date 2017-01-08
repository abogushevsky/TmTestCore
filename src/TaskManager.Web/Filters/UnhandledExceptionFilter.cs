using System.Web.Http.Filters;

namespace TaskManager.Web.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context == null)
                return;
            //TODO: Log exception
        }
    }
}
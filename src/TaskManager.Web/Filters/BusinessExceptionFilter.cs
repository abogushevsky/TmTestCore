using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using TaskManager.Common.Exceptions;

namespace TaskManager.Web.Filters
{
    public class BusinessExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context == null)
                return;

            BusinessException ex = context.Exception as BusinessException;
            if (ex != null)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnException(context), cancellationToken);
        }
    }
}
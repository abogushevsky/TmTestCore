using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    public class TaskManagerApiControllerBase : ApiController
    {
        protected async Task<ApplicationUser> GetCurrentUser()
        {
            if (this.User.Identity.IsAuthenticated)
                return await this.UserManager.FindByNameAsync(this.User.Identity.Name);

            return null;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";
            ApplicationUser user = await GetCurrentUser();
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
            }
            return View();
        }

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

using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TaskManager.Web.Startup))]

namespace TaskManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SimpleInjectorWebApiInitializer.Initialize();
             
            GlobalHost.DependencyResolver =
                (IDependencyResolver)
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IDependencyResolver));

            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

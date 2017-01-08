using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TaskManager.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Обработка ошибок приложения
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Данные события</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception serverError = Server.GetLastError();
            //if (this.Logger != null)
            //{
            //    this.Logger.LogFatal(serverError, "Необработанная ошибка");
            //}
        }
    }
}

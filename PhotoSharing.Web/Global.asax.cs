using PhotoSharing.Web.Models;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PhotoSharing.Web
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<PhotoSharingContext>(new PhotoSharingInitializer());
        }
    }
}
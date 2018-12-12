using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharing.Web
{
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Tipo API Rest
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "Display" },
                constraints: new { id = "[0-9]+" }
            );

            // SEO
            routes.MapRoute(
                name: "PhotoTitleRoute",
                url: "photo/title/{title}",
                defaults: new { controller = "Photo", action = "DisplayByTitle" }
            );

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
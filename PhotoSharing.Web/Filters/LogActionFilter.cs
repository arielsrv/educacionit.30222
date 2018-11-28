using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharing.Web.Controllers
{
    public class LogActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogValues(filterContext.RouteData);
        }

        /// <summary>
        /// Logs the values.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        private void LogValues(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            string message = $"Controller: {controller}; Action: {action}";
            Debug.WriteLine(message, "Action Values");

            foreach (var item in routeData.Values)
            {
                Debug.WriteLine(">> Key: {0}; Value {1}", item.Key, item.Value);
            }
        }
    }
}
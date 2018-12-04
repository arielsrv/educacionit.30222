using System.Web.Mvc;

namespace PhotoSharing.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
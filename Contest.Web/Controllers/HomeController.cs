using Microsoft.AspNetCore.Mvc;

namespace Contest.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
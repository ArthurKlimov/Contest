using Microsoft.AspNetCore.Mvc;

namespace Contest.Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("addContest")]
        public IActionResult AddContest()
        {
            return View();
        }

        [Route("adminPage")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
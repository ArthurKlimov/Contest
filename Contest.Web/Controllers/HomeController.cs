using Contest.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContestService _contestService;

        public HomeController(IContestService contestService)
        {
            _contestService = contestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddContest()
        {
            return View();
        }
    }
}
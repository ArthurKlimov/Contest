using Contest.BL.Dto;
using Contest.BL.Interfaces;
using Contest.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contest.Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IContestService _contestService;

        public HomeController(IContestService contestService)
        {
            _contestService = contestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
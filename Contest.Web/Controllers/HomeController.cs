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
        public async Task<IActionResult> Index([FromQuery] string sort, [FromQuery] string search)
        {
            var contests = await _contestService.GetContests(new GetContestsDto(sort, search));
            var vm = new HomeViewModel(contests.Items, contests.Sort, contests.Search);
            return View(vm);
        }
    }
}
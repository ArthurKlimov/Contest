using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Enums;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
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
        public async Task<IActionResult> Index(string sort = "Popular", string search = "")
        {
            Enum.TryParse(sort, out ContestsSortType sortType);

            var dto = new GetContestsDto(sortType, search, 1, 12);
            var contests = await _contestService.GetContests(dto);

            var vm = new HomeViewModel(contests, sort, search);
            return View(vm);
        }
    }
}
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Contest.Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IContestService _contestService;
        private readonly IImageService _imageService;

        public HomeController(IContestService contestService, IImageService imageService)
        {
            _contestService = contestService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = new GetContestsDto()
            {
                PageNumber = 1,
                PageSize = 10,
                Search = "",
                Sort = BL.Enums.ContestsSortType.Popular
             };

            var contests = await _contestService.GetContests(dto);
            return View(new HomeViewModel(contests.Items));
        }

        [HttpGet]
        [Route("/addContest")]
        public async Task<IActionResult> AddContest()
        {
            return View();
        }

        [HttpPost]
        [Route("/addContest")]
        public async Task<IActionResult> AddContest([FromForm] AddContestDto dto, [FromForm] IFormFile coverImage)
        {
            try
            { 
                var contestId = await _contestService.AddContest(dto);

                if (coverImage != null)
                {
                    var coverImageBytes = coverImage.OpenReadStream().GetBytes();
                    var coverImageType = coverImage.ContentType;

                    await _imageService.UploadContestCover(coverImageBytes, coverImageType, contestId);
                }
            }
            catch (ContestValidationException)
            {
                return BadRequest();
            }
            catch (BadImageFormatException)
            {
                return BadRequest();
            }
            catch (ImageProcessingException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
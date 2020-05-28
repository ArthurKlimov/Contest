using System;
using System.Threading.Tasks;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contest.Web.Controllers
{
    [Route("contests")]
    public class ContestsController : Controller
    {
        private readonly IContestService _contestService;
        private readonly IImageService _imageService;

        public ContestsController(IContestService contestService, IImageService imageService)
        {
            _contestService = contestService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewWithContests(GetContestsDto dto)
        {
            try
            {
                var contests = await _contestService.GetContests(dto);
                return PartialView("../Home/Index/_Contests", contests);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddContest()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
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
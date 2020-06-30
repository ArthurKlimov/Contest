using System;
using System.Threading.Tasks;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.Web.Models;
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
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddContest()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddContest([FromForm] AddContest model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            byte[] coverBytes = null;
            if (model.CoverImage != null && model.CoverImage.ContentType.StartsWith("image/"))
                coverBytes = model.CoverImage.OpenReadStream().GetBytes();

            try
            {
                await _contestService.AddContest(new ContestDto(model.EndDate, model.Title, model.Description, model.Link, model.City, coverBytes));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetContests([FromQuery] GetContestsDto dto)
        {
            try
            {
                return Json(await _contestService.GetContests(dto));
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("contest/{id}")]
        public async Task<IActionResult> GetContest([FromRoute] int id)
        {
            try
            {
                var contest = await _contestService.GetContest(id);
                return View(contest);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
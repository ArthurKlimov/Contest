using System;
using System.Threading.Tasks;
using Contest.BL.Dto;
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

        public ContestsController(IContestService contestService)
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
        public async Task<IActionResult> AddContest(AddContest model)
        {
            if (!ModelState.IsValid || (!model.AcrossCountry && string.IsNullOrWhiteSpace(model.City)))
                return BadRequest();

            byte[] coverBytes = null;

            if (model.CoverImage != null)
            {
                if (!model.CoverImage.ContentType.StartsWith("image/"))
                    return BadRequest();

                coverBytes = model.CoverImage.OpenReadStream().GetBytes();
                if (coverBytes.Length > 5242880)
                    return BadRequest();
            }

            try
            {
                await _contestService.AddContest(new ContestDto(model.EndDate, model.Title, model.Link, 
                    model.City, model.AcrossCountry, coverBytes));
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
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{id}/publish")]
        public async Task<IActionResult> PublishContest([FromRoute] int id)
        {
            try
            {
                await _contestService.PublishContest(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("contest/{id}/hide")]
        public async Task<IActionResult> HideContest([FromRoute] int id)
        {
            try
            {
                await _contestService.HideContest(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("contest/{id}")]
        public async Task<IActionResult> DeleteContest([FromRoute] int id)
        {
            try
            {
                await _contestService.DeleteContest(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ContestsController(IContestService contestService, IMapper mapper)
        {
            _contestService = contestService;
            _mapper = mapper;
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

            try
            {
                await _contestService.AddContest(_mapper.Map<AddContest, ContestDto>(model));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("all")]
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
        [Route("contests/{id}")]
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
        [Route("{id}/hide")]
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
        [Route("{id}")]
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
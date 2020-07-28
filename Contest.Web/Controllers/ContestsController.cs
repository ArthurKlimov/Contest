using System;
using System.Text.Encodings.Web;
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
        private readonly HtmlEncoder _htmlEncoder;


        public ContestsController(IContestService contestService, IMapper mapper, 
                                  HtmlEncoder htmlEncoder, UrlEncoder urlEncoder)
        {
            _contestService = contestService;
            _mapper = mapper;
            _htmlEncoder = htmlEncoder;
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddContest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContest(AddContest model)
        {
            if (!ModelState.IsValid || (!model.AcrossCountry && string.IsNullOrWhiteSpace(model.City)))
                return BadRequest();

            var dto = _mapper.Map<AddContest, ContestDto>(model);
            await _contestService.AddContest(dto);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetContests([FromQuery] GetContestsDto dto)
        {
            if (!dto.IsValid())
                return BadRequest();

            var contests = await _contestService.GetContests(dto);

            return Json(contests);
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
    }
}
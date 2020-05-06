using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contest.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _contestService;
        public ContestController(IContestService contestService)
        {
            _contestService = contestService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContest(AddContestDto dto)
        {
            try
            {
                await _contestService.AddContest(dto);
                return Ok();
            }
            catch (ContestValidationException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetContest(BaseContestDto dto)
        {
            try
            {
                var contest = await _contestService.GetContest(dto);
                return Ok(contest);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("paged")]
        public async Task<IActionResult> GetAllContests(GetAllContestsDto dto)
        {
            try
            {
                var contests = await _contestService.GetAllContests(dto);
                return Ok(contests);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditContest(ContestDto dto)
        {
            try
            {
                await _contestService.EditContest(dto);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ContestValidationException)
            {
                return NotFound();
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteContest(BaseContestDto dto)
        {
            try
            {
                await _contestService.DeleteContest(dto);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
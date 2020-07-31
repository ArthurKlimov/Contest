using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contest.Web.Controllers.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContestsController : ControllerBase
    {
        private readonly IContestService _contestService;
        private readonly HtmlEncoder _htmlEncoder;

        public ContestsController(IContestService contestService, HtmlEncoder htmlEncoder)
        {
            _contestService = contestService;
            _htmlEncoder = htmlEncoder;
        }

        [HttpPost]
        public async Task<IActionResult> AddContest(AddContestDto dto)
        {
            try
            {
                await _contestService.AddContest(dto);
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest("Неправильный формат одного или нескольких параметров");
            }
        }

        public async Task<IActionResult> GetContests([FromQuery]GetContestsDto dto)
        {
            try
            {
                var contests = await _contestService.GetContests(dto);
                return Ok(contests);
            }
            catch (ValidationException)
            {
                return BadRequest("Неправильный формат одного или нескольких параметров");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteContest(int id)
        {
            try
            {
                await _contestService.DeleteContest(id);
                return Ok();
            }
            catch(NotFoundException)
            {
                return NotFound("Конкурс не найден");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContest(ContestDto dto)
        {
            try
            {
                await _contestService.UpdateContest(dto);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("Конкурс не найден");
            }
            catch (ValidationException)
            {
                return BadRequest("Неправильный формат одного или нескольких параметров");
            }
        }
    }
}

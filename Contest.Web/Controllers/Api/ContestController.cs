using Contest.BL.Dto;
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
        public async Task<IActionResult> AddContest(ContestDto dto)
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
    }
}
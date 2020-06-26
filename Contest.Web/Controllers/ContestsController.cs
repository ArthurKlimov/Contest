using System;
using System.Threading.Tasks;
using Contest.BL.Dto;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.Web.Models;
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
            if (model.FormFile != null && model.FormFile.ContentType.StartsWith("image/"))
                coverBytes = model.FormFile.OpenReadStream().GetBytes();

            try
            {
                await _contestService.AddContest(new ContestDto(model.EndDate, model.Title, model.Description, model.Link, coverBytes));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
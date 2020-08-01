using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contest.DA;
using Contest.DA.Entities;
using Contest.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contest.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ContestContext _db;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, 
                              ContestContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password))
                return View("Login", "Вы не ввели данные");

            var result = await _signInManager.PasswordSignInAsync(dto.Login, dto.Password, true, false);
            if (result.Succeeded)
                return Redirect("/adminPage");

            return View("Login", "Ошибка авторизации");
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            var newUser = new User
            {
                UserName = model.Login,
                Email = model.Email
            };

            var user = await _db.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == model.Email.ToUpper());
            if (user != null)
            {
                return BadRequest();
            }

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}

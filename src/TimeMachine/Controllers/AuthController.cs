using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TimeMachine.Models;
using TimeMachine.ViewModels;

namespace TimeMachine.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            // TODO needs a better way to expire cookies when webapp is restarted.
            signInManager.SignOutAsync();
        }

        [HttpPost("register")]
        public async Task<string> Register([FromBody] LoginViewModel model)
        {
            var email = model.EmailAddress;
            var password = model.Password;
            
            var result = await _userManager.CreateAsync
            (
                new User() { UserName = email, Email = email },
                password
            );

            if (result.Succeeded)
            {
                return "[OK]";
            }
            else
            {
                return "[FAILED]";
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync
            (
                model.EmailAddress,
                model.Password,
                false,
                false
            );

            if (result.Succeeded)
            {
                return Ok("ok");
            }
            else
            {
                return Unauthorized("failed");
            }
        }

        [HttpPost("logout")]
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return "ok";
        }
    }
}

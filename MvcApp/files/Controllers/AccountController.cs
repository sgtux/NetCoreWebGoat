using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebGoat.Helpers;
using NetCoreWebGoat.Models;
using NetCoreWebGoat.Repositories;

namespace NetCoreWebGoat.Controllers
{
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private UserRepository _userRepository;

        private readonly Stopwatch _watcher;

        public AccountController(ILogger<AccountController> logger, UserRepository userRepository) : base(logger)
        {

            _userRepository = userRepository;
            _watcher = new Stopwatch();
            _watcher.Start();
        }

        [HttpGet("Login")]
        public IActionResult Login([FromQuery]string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = _userRepository.Login(model);
            if (user is null)
            {
                ModelState.AddModelError(nameof(model.Password), "Invalid credentials");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            if (!string.IsNullOrEmpty(user.Photo))
                claims.Add(new Claim("Photo", user.Photo));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? "/" : model.ReturnUrl);
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.Confirm)
                    ModelState.AddModelError(nameof(model.Confirm), $"The fields '{nameof(model.Password)}' and '{nameof(model.Confirm)}' don't match.");

                if (_userRepository.FindByEmail(model.Email) != null)
                    ModelState.AddModelError(nameof(model.Email), "Already has a registed user with this Email");

                if (ModelState.IsValid)
                {
                    _userRepository.Register(model);

                    var user = _userRepository.FindByEmail(model.Email);

                    var claims = new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
                    return Redirect("/");
                }
            }

            return View();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

        [HttpGet("Edit")]
        public IActionResult Edit() => View();

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UserChangeModel model)
        {
            model.Photo = HashHelper.Md5(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")).Substring(0, 6) + model.File.FileName;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", model.Photo);
            if (model.File.Length == 0)
            {
                ModelState.AddModelError(nameof(model.File), "Invalid file.");
            }

            if (ModelState.IsValid)
            {
                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                _userRepository.UpdatePhoto(UserId, model.Photo);

                var claims = new List<Claim>();
                claims.AddRange(User.Claims);
                claims.Add(new Claim("Photo", model.Photo));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
                return Redirect("/");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _userRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
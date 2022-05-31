using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebGoat.Auth;
using NetCoreWebGoat.Config;
using NetCoreWebGoat.Models;
using NetCoreWebGoat.Repositories;

namespace NetCoreWebGoat.Controllers
{
    [Route("[controller]")]
    public class TokenController : BaseController
    {
        private UserRepository _userRepository;

        private readonly Stopwatch _watcher;

        private readonly AppConfig _appConfig;

        public TokenController(ILogger<TokenController> logger, UserRepository userRepository, AppConfig appConfig) : base(logger)
        {
            _userRepository = userRepository;
            _appConfig = appConfig;
            _watcher = new Stopwatch();
            _watcher.Start();
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            var user = _userRepository.Login(model);
            if (user is null)
                return Unauthorized("Invalid credentials");

            var token = new JwtTokenBuilder(_appConfig.JwtKey, _appConfig.CookieExpiresInMinutes, user.Claims).Build().Value;
            return Ok(new { token = token });
        }

        protected override void Dispose(bool disposing)
        {
            _userRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
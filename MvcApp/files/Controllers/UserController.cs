using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebGoat.Models;
using NetCoreWebGoat.Repositories;

namespace NetCoreWebGoat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private UserRepository _userRepository;

        public UserController(UserRepository userRepository, ILogger<UserController> logger) : base(logger) => _userRepository = userRepository;

        [HttpGet]
        public IActionResult Flag()
        {
            if (IsAdmin)
            {
                var users = _userRepository.FindAll();
                return Ok(users.Select(p => new UserModelResult(p)));
            }

            return Forbid();
        }
    }
}
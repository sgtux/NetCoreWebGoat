using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebGoat.Models;
using NetCoreWebGoat.Config;

namespace NetCoreWebGoat.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SecurityController : BaseController
    {
        private AppConfig _appConfig;
        public SecurityController(ILogger<PostController> logger, AppConfig appConfig) : base(logger)
        {
            _appConfig = appConfig;
        }

        [HttpGet]
        public IActionResult Index() => View(new AppConfigModel(_appConfig));

        [HttpPost]
        public IActionResult Index(AppConfigModel model)
        {
            _appConfig.Update(model);
            return View();
        }
    }
}
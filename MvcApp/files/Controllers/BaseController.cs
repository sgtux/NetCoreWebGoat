using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreWebGoat.Controllers
{
    public class BaseController : Controller
    {
        private readonly Stopwatch _watcher;

        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _watcher = new Stopwatch();
            _watcher.Start();
            _logger = logger;
        }

        public int UserId => Convert.ToInt32(User.Claims.FirstOrDefault(p => p.Type == "Id")?.Value ?? "0");
        
        public bool IsAdmin => User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value == "1";

        protected override void Dispose(bool disposing)
        {
            _watcher.Stop();
            var ts = _watcher.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            _logger.LogInformation($"{Request.Path} - {elapsedTime}");
        }
    }
}
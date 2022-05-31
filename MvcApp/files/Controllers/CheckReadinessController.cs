using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreWebGoat.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CheckReadinessController : BaseController
    {
        public CheckReadinessController(ILogger<CheckReadinessController> logger) : base(logger) { }

        [HttpGet]
        public IActionResult Index([FromQuery] string ip)
        {
            if (string.IsNullOrEmpty(ip))
                ip = "127.0.0.1";
            var process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"ping -c1 {ip}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            var resultOutput = process.StandardOutput.ReadToEnd();
            var resultError = process.StandardError.ReadToEnd();
            process.WaitForExit();
            ViewBag.Result = $"{resultOutput} - {resultError}";
            ViewBag.Ip = ip;
            return View();
        }
    }
}
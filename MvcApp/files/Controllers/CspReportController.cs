using Microsoft.AspNetCore.Mvc;
using NetCoreWebGoat.Models;
using NetCoreWebGoat.Repositories;

namespace NetCoreWebGoat.Controllers
{
    public class CspReportController : Controller
    {
        private CspRepository _cspRepository;

        public CspReportController(CspRepository cspRepository)
        {
            _cspRepository = cspRepository;
        }

        [HttpGet]
        public IActionResult Index() => View(_cspRepository.GetAll());

        [HttpPost]
        public IActionResult Index([FromBody] CspReportModel model)
        {
            _cspRepository.Add(model.CspReport);
            return Ok();
        }
    }
}
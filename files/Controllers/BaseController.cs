using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebGoat.Controllers
{
    public class BaseController : Controller
    {
        public int UserId => Convert.ToInt32(User.Claims.FirstOrDefault(p => p.Type == "Id")?.Value ?? "0");
    }
}

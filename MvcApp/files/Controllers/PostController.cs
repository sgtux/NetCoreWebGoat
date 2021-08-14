using NetCoreWebGoat.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebGoat.Models;
using System.IO;
using System.Threading.Tasks;
using NetCoreWebGoat.Helpers;
using System;
using Microsoft.Extensions.Logging;

namespace NetCoreWebGoat.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PostController : BaseController
    {
        private readonly PostRepository _postRepository;

        public PostController(ILogger<PostController> logger, PostRepository postRepository) : base(logger)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string search)
        {
            ViewBag.Search = search ?? "";
            var posts = _postRepository.GetAll(search);
            posts.ForEach(p => p.Owner = p.UserId == UserId);
            return View(posts);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(PostModel model)
        {
            if (model.File is null || model.File.Length == 0)
            {
                ModelState.AddModelError(nameof(model.File), "Invalid file.");
            }

            if (ModelState.IsValid)
            {
                model.Photo = HashHelper.Md5(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")).Substring(0, 6) + model.File.FileName;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", model.Photo);
                model.UserId = UserId;
                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                _postRepository.Add(model);
                return Redirect("/Post");
            }
            return View();
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _postRepository.Delete(id);
            return Redirect("/Post");
        }

        protected override void Dispose(bool disposing)
        {
            _postRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
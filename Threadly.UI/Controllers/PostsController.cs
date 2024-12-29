using Microsoft.AspNetCore.Mvc;
using Threadly.UI.DTOs.Posts;

namespace Threadly.UI.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePost createPost)
        {
            TempData["Content"] = createPost.quillContent;
            TempData["ActionUrl"] = "/Posts/CreatePost";

            return RedirectToAction("Index", "Home");

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Threadly.UI.Models.ViewModels.Post;

namespace Threadly.UI.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostVM createPostVM)
        {
            TempData["Content"] = createPostVM.quillContent;
            TempData["ActionUrl"] = "/Posts/CreatePost";

            return RedirectToAction("Index", "Home");

        }

    }
}

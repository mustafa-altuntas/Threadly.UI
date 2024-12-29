using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.PostEditorVC
{
    public class PostEditorVC:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string content = "", string actionUrl = "/Posts/CreatePost")
        {



            // "content" mevcut makale verisini düzenlemek için kullanılacak.
            ViewData["Content"] = content;
            ViewData["ActionUrl"] = actionUrl;


            return View();
        }
    }
}

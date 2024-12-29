using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.Sidebar
{
    public class Sidebar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

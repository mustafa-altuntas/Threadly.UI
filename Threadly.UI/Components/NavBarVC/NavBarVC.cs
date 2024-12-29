using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.NavBarVC
{
    public class NavBarVC:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

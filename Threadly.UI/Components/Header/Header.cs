using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.Shared.Header
{
    public class Header:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

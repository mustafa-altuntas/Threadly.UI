using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.Shared.Footer
{
    public class Footer : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

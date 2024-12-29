using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Components.ScriptVC
{
    public class ScriptVC : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

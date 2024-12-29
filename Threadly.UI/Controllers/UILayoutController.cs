using Microsoft.AspNetCore.Mvc;

namespace Threadly.UI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

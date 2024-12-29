using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Threadly.UI.Models;
using Threadly.UI.Models.ViewModels;

namespace Threadly.UI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (TempData.ContainsKey("ToastifyMessage"))
                ViewBag.ToastifyMessage = TempData["ToastifyMessage"];

            return View();
        }

        [HttpGet]
        public IActionResult Editor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editor(PostDeneme post)
        {
            return View();
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Threadly.UI.Models;
using Threadly.UI.Models.ViewModels;

namespace Threadly.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

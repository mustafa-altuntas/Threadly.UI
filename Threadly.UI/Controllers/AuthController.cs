using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.Models.ViewModels.TokenVM;
using Threadly.UI.Models.ViewModels.Users;
using Threadly.UI.Models.ViewModels.Users.LoginUser;
using Threadly.UI.Services.Abstracts;

namespace Threadly.UI.Controllers
{

    public class AuthController : Controller
    {
        private readonly IApiService _apiService;

        public AuthController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserVM createUser)
        {
 
            if (!ModelState.IsValid)
            {
                return View(createUser);
            }

            var result = await _apiService.PostAsync<CreateUserVM, CreateUserResultVM>("Users/CreateUser", createUser);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(createUser);
            }

            TempData["SuccessMessage"] = $"Kullanıcı başarıyla kaydedildi. \n {createUser.Email}";
            return RedirectToAction("Login");
 

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserVM loginUserVM)
        {

            if (!ModelState.IsValid)
            {
                return View(loginUserVM);
            }

            var result = await _apiService.PostAsync<LoginUserVM, TokenResponse>("Users/LoginUser", loginUserVM);

            TempData["ToastifyMessage"] = "Kullanıcı başarıyla giriş yaptı!";
            return RedirectToAction("Index", "Home");

        }
    }
}

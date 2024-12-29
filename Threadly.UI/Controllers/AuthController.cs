using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
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

            var result = await _apiService.PostAsync<LoginUserVM, LoginUserResult>("Users/LoginUser", loginUserVM);

            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError(string.Empty, "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
            //    return View(loginUserVM);
            //}

            // Giriş başarılı
            //TempData["SuccessMessage"] = "Giriş başarılı. Hoş geldiniz!";
            return RedirectToAction("Index", "Home");




        }
    }
}

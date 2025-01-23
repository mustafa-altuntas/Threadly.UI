using Microsoft.AspNetCore.Mvc;
using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.Helpers;
using Threadly.UI.Models.ViewModels.Users;
using Threadly.UI.Models.ViewModels.Users.LoginUser;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.DTOs;

namespace Threadly.UI.Controllers
{

    public class AuthController : Controller
    {
        private readonly IApiService _apiService;

        public AuthController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserVM createUser)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(createUser);
            //}

            var result = await _apiService.PostAsync<CreateUserVM, ResponseDto<NoDataDto>>("Users/CreateUser", createUser);

            if (!result.IsSuccessfull)
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

            var result = await _apiService.PostAsync<LoginUserVM, ResponseDto<TokenResponse>>("Auth/LoginUser", loginUserVM);


            var signInSuccessful = await CookieHelper.SignInAsync( HttpContext, result.Data);

            string message = $"\nA: {result.Data.AccessToken.Expiration} \nR: {result.Data.RefreshToken.Expiration}";

            TempData["ToastifyMessage"] = signInSuccessful ? "Kullanıcı başarıyla giriş yaptı!"+ message : "Kullanıcı giriş yaparken bir hata oluştu!"+ message;


            return RedirectToAction("Index", "Home");


            //var userId = HttpContext.User.Claims;
            //var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("email"))!.Value;

        }
    }
}

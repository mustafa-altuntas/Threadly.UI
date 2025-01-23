using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.DTOs;
using Threadly.UI.Helpers;
using Threadly.UI.Models.ViewModels.Users.LoginUser;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.Services.Abstracts.AuthService;

namespace Threadly.UI.Services.Concretes.AuthService
{
    public class SignInService : ISignInService
    {
        private readonly IApiService _apiService;

        public SignInService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoginRefreshTokenAsync(string refreshToken, HttpContext context)
        {
            try
            {
                var result = await _apiService.PostAsync<string, ResponseDto<TokenResponse>>("Auth/LoginUser", refreshToken);

                var signInSuccessful = await CookieHelper.SignInAsync(context, result.Data);
            }
            catch (Exception)
            {

                
            }



        }
    }
}

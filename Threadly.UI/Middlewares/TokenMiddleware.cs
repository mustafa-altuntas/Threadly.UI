using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Threadly.UI.Constants;
using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.Helpers;
using Threadly.UI.Models.Tokens;
using Threadly.UI.Services.Abstracts;

namespace Threadly.UI.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiService _apiService;

        public TokenMiddleware(RequestDelegate next, IApiService apiService)
        {
            _next = next;
            _apiService = apiService;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!context.Request.Cookies.ContainsKey(CookieConstant.CookieName))
            {

                await _next(context);
            }

            var tokenResponse = GetToken(context);


            if (IsTokenExpired(tokenResponse.AccessToken))
            {
                if (IsTokenExpired(tokenResponse.RefreshToken))
                {
                    await context.SignOutAsync(); //Refresh token süresi dolduysa çıkış yap !!Cookie ömrü refresh token ile aynı olduğu için zan silinecek ve çıkış yapacak yani bu işlem biraz gereksiz.
                }

                if (!string.IsNullOrEmpty(tokenResponse.RefreshToken.Code))
                {
                    // Refresh token varsa çalışır
                    var newTokenResponse = await LoginRefreshTokenAsync(tokenResponse.RefreshToken);
                    if (newTokenResponse != null)
                    {
                        CookieHelper.SetClaims( newTokenResponse);
                        await CookieHelper.SignInAsync(context,newTokenResponse);
                    }
                    else
                    {
                        RedirectToLogin(context);
                        return;
                    }

                }
                else
                {
                    // Refresh token yoksa çalışır.
                    await _next(context);

                }

            }

            await _next(context);

        }

        private void RedirectToLogin(HttpContext context)
        {
            CookieHelper.RemoveCookie(context, "AccessToken");
            CookieHelper.RemoveCookie(context, "AccessTokenExpiration");
            CookieHelper.RemoveCookie(context, "AccessToken");
            CookieHelper.RemoveCookie(context, "RefreshTokenExpiration");
            context.Response.Redirect("/Auth/Login");
        }

        async Task<TokenResponse> LoginRefreshTokenAsync(Token refreshToken)
        {
            var response = await _apiService.PostAsync<string, TokenResponse>("Users/LoginUserRefreshToken", refreshToken.Code);

            return response;
        }

         bool IsTokenExpired(Token token)
        {
            return token.Expiration < DateTime.UtcNow;
        }


        TokenResponse GetToken(HttpContext context)
        {
            TokenResponse tokenResponse = new TokenResponse { AccessToken = new(), RefreshToken = new() };
            DateTime dateValue;

            tokenResponse.AccessToken.Code = context.User.FindFirst(CookieConstant.AccessToken)?.Value;
            DateTime.TryParse(context.User?.FindFirst(CookieConstant.AccessTokenExpiration)?.Value, out dateValue);
            if (dateValue != DateTime.MinValue)
                tokenResponse.AccessToken.Expiration = dateValue;

            tokenResponse.RefreshToken.Code = context.User?.FindFirst(CookieConstant.RefreshToken)?.Value;
            DateTime.TryParse(context.User?.FindFirst(CookieConstant.RefreshTokenExpiration)?.Value, out dateValue);
            if (dateValue != DateTime.MinValue)
                tokenResponse.RefreshToken.Expiration = dateValue;



            return tokenResponse;
        }


    }
}

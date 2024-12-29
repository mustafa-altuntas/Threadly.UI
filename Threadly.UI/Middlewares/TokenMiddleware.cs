using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.Helpers;
using Threadly.UI.Models.Tokens;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.Services.Concretes;

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
            var tokenResponse = new TokenResponse() { AccessToken = new(), RefreshToken = new() };
            DateTime dateValue;

            tokenResponse.AccessToken.Code = CookieHelper.GetCookie(context, "AccessToken");
            DateTime.TryParse(CookieHelper.GetCookie(context, "AccessTokenExpiration"), out dateValue);
            if (dateValue != DateTime.MinValue)
                tokenResponse.AccessToken.Expiration = dateValue;

            tokenResponse.RefreshToken.Code = CookieHelper.GetCookie(context, "RefreshToken");

            DateTime.TryParse(CookieHelper.GetCookie(context, "RefreshTokenExpiration"), out dateValue);
            if (dateValue != DateTime.MinValue)
                tokenResponse.RefreshToken.Expiration = dateValue;


            if (!string.IsNullOrEmpty(tokenResponse.AccessToken.Code) && IsTokenExpired(tokenResponse.AccessToken))
            {
                if (!string.IsNullOrEmpty(tokenResponse.RefreshToken.Code))
                {
                    var newTokenResponse = await RefreshTokenAsync(tokenResponse.RefreshToken);
                    if (newTokenResponse != null)
                    {
                        CookieHelper.SetCookie(context, newTokenResponse);
                    }
                    else
                    {
                        RedirectToLogin(context);
                        return;
                    }

                }
                else
                {
                    RedirectToLogin(context);
                    return;
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

        public async Task<TokenResponse> RefreshTokenAsync(Token refreshToken)
        {
            var response = await _apiService.PostAsync<string, TokenResponse>("Users/LoginUserRefreshToken", refreshToken.Code);

            return response;
        }

        public bool IsTokenExpired(Token token)
        {
            return token.Expiration < DateTime.UtcNow;
        }


    }
}

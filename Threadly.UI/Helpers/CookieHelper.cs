using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Threadly.UI.Constants;
using Threadly.UI.DTOs.TokenDtos;

namespace Threadly.UI.Helpers
{
    public static class CookieHelper
    {

        public static async Task<bool> SignInAsync(HttpContext context, TokenResponse tokenResponse)
        {



            var claims = SetClaims(tokenResponse);

            try
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                // Oturum bilgilerini oluştur ve Cookie'ye yaz
                var authProperties = new AuthenticationProperties
                {

                    IsPersistent = true, // Çerez kalıcı olsun mu
                    ExpiresUtc = tokenResponse.RefreshToken.Expiration //token'ın ömrü
                };

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return true;

            }
            catch (Exception)
            {

                return false;
            }



        }



        public static List<Claim> SetClaims(TokenResponse tokenResponse)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(CookieConstant.AccessToken, tokenResponse.AccessToken.Code));
            claims.Add(new Claim(CookieConstant.AccessTokenExpiration, tokenResponse.AccessToken.Expiration.ToString("o")));

            claims.Add(new Claim(CookieConstant.RefreshToken, tokenResponse.RefreshToken.Code));
            claims.Add(new Claim(CookieConstant.RefreshTokenExpiration, tokenResponse.RefreshToken.Expiration.ToString("o")));

            return claims;
        }

        public static string? GetCookie(HttpContext context, string key)
        {
            context.Request.Cookies.TryGetValue(key, out var value);
            return value;
        }

        public static void RemoveCookie(HttpContext context, string key)
        {
            context.Response.Cookies.Delete(key);
        }







    }
}

using Threadly.UI.DTOs.TokenDtos;

namespace Threadly.UI.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie(HttpContext context, TokenResponse tokenResponse)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokenResponse.AccessToken.Expiration,
            };
            context.Response.Cookies.Append("AccessToken", tokenResponse.AccessToken.Code, options);
            context.Response.Cookies.Append("AccessTokenExpiration", tokenResponse.AccessToken.Expiration.ToString(), options);
            if (tokenResponse.RefreshToken != null)
            {
                context.Response.Cookies.Append("RefreshToken", tokenResponse.RefreshToken.Code, options);
                context.Response.Cookies.Append("RefreshTokenExpiration", tokenResponse.RefreshToken.Expiration.ToString(), options);

            }
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

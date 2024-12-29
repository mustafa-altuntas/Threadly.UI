using Threadly.UI.DTOs.TokenDtos;
using Threadly.UI.Models.Tokens;
using Threadly.UI.Models.ViewModels.Users.LoginUser;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.Services.Concretes;

namespace Threadly.UI.Helpers
{
    public static class JwtHelper
    {

        public static bool IsTokenExpired(Token token)
        {
            return token.Expiration < DateTime.UtcNow;
        }
    }
}

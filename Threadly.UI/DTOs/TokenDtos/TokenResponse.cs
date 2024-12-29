using Threadly.UI.Models.Tokens;

namespace Threadly.UI.DTOs.TokenDtos
{
    public class TokenResponse
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }
}

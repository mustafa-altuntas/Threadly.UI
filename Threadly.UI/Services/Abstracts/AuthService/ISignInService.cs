namespace Threadly.UI.Services.Abstracts.AuthService
{
    public interface ISignInService
    {
        Task LoginRefreshTokenAsync(string refreshToken, HttpContext context);
    }
}

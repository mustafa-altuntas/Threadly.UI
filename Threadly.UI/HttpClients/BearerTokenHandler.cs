
using Threadly.UI.Helpers;

namespace Threadly.UI.HttpClients
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BearerTokenHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = CookieHelper.GetCookie(_contextAccessor.HttpContext, "AccessToken");
            if(!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            return base.SendAsync(request, cancellationToken);
        }

    }
}

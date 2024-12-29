using Threadly.UI.Configurations;

namespace Threadly.UI.Extensions
{
    public static class ImageBaseUrlExtension
    {
        public static string ToFullImageUrl(this string imageUrl)
        {
            return $"{AppSettings.Configuration["ImageBaseUrl"]}/{imageUrl}";
        }
    }
}

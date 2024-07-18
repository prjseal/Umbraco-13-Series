using Umbraco.Cms.Core.Models;

namespace Freelancer.Extensions
{
    public static class MediaWithCropsExtensions
    {
        public static string GetAltText(this MediaWithCrops mediaItem, string altTextAlias = "altText")
        {
            var altText = mediaItem.Value<string>(altTextAlias);

            if (string.IsNullOrWhiteSpace(altText)) return string.Empty;

            return altText;
        }
    }
}

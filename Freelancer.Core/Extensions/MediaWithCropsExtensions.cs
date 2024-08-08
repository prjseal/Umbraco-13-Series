using Umbraco.Cms.Core.Models;

namespace Freelancer.Core.Extensions;

public static class MediaWithCropsExtensions
{
    public static string GetAltText(this MediaWithCrops mediaItem, string altTextAlias = "altText")
    {
        var altText = mediaItem.Value<string>(altTextAlias);

        return string.IsNullOrWhiteSpace(altText) ? string.Empty : altText;
    }
}
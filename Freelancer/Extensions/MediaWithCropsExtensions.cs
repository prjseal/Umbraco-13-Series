namespace Freelancer.Extensions;

public static class MediaWithCropsExtensions
{
    public static string GetAltText(this MediaWithCrops mediaItem, string altTextAlias = "altText")
    {
        var altText = mediaItem.Value<string>(altTextAlias);

        return string.IsNullOrWhiteSpace(altText) ? string.Empty : altText;
    }
}
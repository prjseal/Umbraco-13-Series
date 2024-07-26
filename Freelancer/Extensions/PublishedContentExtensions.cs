namespace Freelancer.Extensions;

public static class PublishedContentExtensions
{
    public static HomePage? GetHomePage(this IPublishedContent publishedContent)
    {
        return publishedContent.AncestorOrSelf<HomePage>();
    }

    public static SiteSettings? GetSiteSettings(this IPublishedContent publishedContent)
    {
        var homePage = GetHomePage(publishedContent);
        return homePage?.FirstChild<SiteSettings>();
    }

}
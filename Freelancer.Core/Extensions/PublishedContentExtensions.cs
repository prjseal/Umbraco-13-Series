using Freelancer.Core.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Freelancer.Core.Extensions;

public static class PublishedContentExtensions
{
    public static HomePage? GetHomePage(this IPublishedContent publishedContent)
    {
        return publishedContent.AncestorOrSelf<HomePage>();
    }

    public static SiteSettings? GetSiteSettings(this IPublishedContent publishedContent)
    {
        var homePage = publishedContent.GetHomePage();
        return homePage?.FirstChild<SiteSettings>();
    }

    public static string GetMetaTitleOrName(this IPublishedContent publishedContent, string? metaTitle)
    {
        if (!string.IsNullOrWhiteSpace(metaTitle)) return metaTitle;

        return publishedContent.Name;
    }

    public static string? GetSiteName(this IPublishedContent publishedContent)
    {
        var homePage = publishedContent.GetHomePage();
        if (homePage == null) return null;
        var siteSettings = homePage.GetSiteSettings();
        if (siteSettings == null) return null;
        return siteSettings?.SiteName ?? null;
    }

    public static IEnumerable<SelectListItem>? GetPageTagsSelectList(this IPublishedContent publishedContent)
    {
        IEnumerable<SelectListItem>? allTags = null;

        var siteSettings = publishedContent.GetSiteSettings();

        if (siteSettings == null) return null;

        var pageTagsContainer = siteSettings.FirstChildOfType(PageTags.ModelTypeAlias);
        if (pageTagsContainer?.Children != null && pageTagsContainer.Children.Any())
        {
            var pageTags = pageTagsContainer.Children.Select(x => x as PageTag).Where(y => y != null);
            allTags = pageTags.Select(x => new SelectListItem() { Text = x!.Name, Value = x.TagAlias });
        }

        return allTags;
    }
}
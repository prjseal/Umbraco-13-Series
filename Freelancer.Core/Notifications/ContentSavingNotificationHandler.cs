using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Freelancer.Core.Notifications;

public class ContentSavingNotificationHandler(IShortStringHelper shortStringHelper) : INotificationHandler<ContentSavingNotification>
{
    private readonly IShortStringHelper _shortStringHelper = shortStringHelper;

    public void Handle(ContentSavingNotification notification)
    {
        foreach (var node in notification.SavedEntities)
        {
            if (node.ContentType.Alias.Equals(PageTag.ModelTypeAlias) && node.Id == 0)
            {
                var safeAlias = node?.Name?.ToSafeAlias(_shortStringHelper);
                node?.SetValue("tagAlias", safeAlias);
            }
        }
    }
}
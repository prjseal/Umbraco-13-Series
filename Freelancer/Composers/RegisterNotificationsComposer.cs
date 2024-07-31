using Freelancer.Notifications;

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

namespace Freelancer.Composers;

public class RegisterNotificationsComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<ContentSavingNotification, ContentSavingNotificationHandler>();
    }
}
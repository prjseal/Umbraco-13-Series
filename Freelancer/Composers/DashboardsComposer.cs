using Our.Umbraco.TheDashboard.Counters.Implement;
using Our.Umbraco.TheDashboard.Extensions;

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Freelancer.Composers;

public class DashboardsComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Dashboards().Remove<ContentDashboard>();
        builder.TheDashboardCounters().Remove<MembersTotalDashboardCounter>()
            .Remove<MembersNewLastWeekDashboardCounter>();
    }
}
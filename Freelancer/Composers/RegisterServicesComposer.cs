using Freelancer.Services;

using Umbraco.Cms.Core.Composing;

namespace Freelancer.Composers;

public class RegisterServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ISearchService, SearchService>();
    }
}
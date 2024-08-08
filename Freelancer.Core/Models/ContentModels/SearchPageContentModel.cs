using Freelancer.Core.Models.Search;
using Freelancer.Core.Models.ViewModels;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Freelancer.Core.Models.ContentModels;

public class SearchPageContentModel(IPublishedContent? content) : ContentModel(content)
{
    public SearchRequestModel? SearchRequest { get; set; }
    public SearchResponseModel? SearchResponse { get; set; }
    public PaginationViewModel Pagination { get; set; }
}
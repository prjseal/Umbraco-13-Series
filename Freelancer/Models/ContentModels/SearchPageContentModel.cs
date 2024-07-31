
using Freelancer.Models.Search;
using Freelancer.Models.ViewModels;

namespace Freelancer.Models.ContentModels;

public class SearchPageContentModel(IPublishedContent? content) : ContentModel(content)
{
    public SearchRequestModel? SearchRequest { get; set; }
    public SearchResponseModel? SearchResponse { get; set; }
    public PaginationViewModel Pagination { get; set; }
}
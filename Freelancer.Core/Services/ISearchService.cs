using Freelancer.Core.Models.Search;

namespace Freelancer.Core.Services;

public interface ISearchService
{
    public SearchResponseModel Search(SearchRequestModel searchRequest);
}
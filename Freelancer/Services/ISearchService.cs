using Freelancer.Models.Search;

namespace Freelancer.Services;

public interface ISearchService
{
    public SearchResponseModel Search(SearchRequestModel searchRequest);
}
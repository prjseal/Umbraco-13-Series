using Examine;

namespace Freelancer.Models.Search;

public class SearchResponseModel
{
    public bool HasResults => TotalResultCount > 0;
    public string? Query { get; set; }
    public long TotalResultCount { get; set; }
    public IEnumerable<ISearchResult>? SearchResults { get; set; }

    public SearchResponseModel() { }

    public SearchResponseModel(string? query, long totalResultCount, IEnumerable<ISearchResult>? searchResults)
    {
        Query = query;
        TotalResultCount = totalResultCount;
        SearchResults = searchResults;
    }
}
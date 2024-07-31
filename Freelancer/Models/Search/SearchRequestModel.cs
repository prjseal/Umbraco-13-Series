namespace Freelancer.Models.Search;

public class SearchRequestModel
{
    public string? Query { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Skip => Page > 1 ? (Page - 1) * PageSize : 0;

    public SearchRequestModel(string? query, string? page, int pageSize)
    {
        Query = query;

        if (int.TryParse(page, out int pageNumber) && pageNumber > 0)
        {
            Page = pageNumber;
        }

        PageSize = pageSize;
    }
}
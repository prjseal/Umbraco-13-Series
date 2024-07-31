using Microsoft.AspNetCore.Mvc.Rendering;

namespace Freelancer.Models.Search;

public class SearchRequestModel
{
    public string? Query { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Skip => Page > 1 ? (Page - 1) * PageSize : 0;
    public IEnumerable<SelectListItem>? AllTags { get; set; }
    public string? SelectedTags { get; set; }


    public SearchRequestModel(string? query, string? page, int pageSize, string? selectedTags, IEnumerable<SelectListItem>? allTags)
    {
        Query = query;

        if (int.TryParse(page, out int pageNumber) && pageNumber > 0)
        {
            Page = pageNumber;
        }

        PageSize = pageSize;

        SelectedTags = selectedTags;

        AllTags = allTags?.Select(item =>
                    new SelectListItem
                    {
                        Value = item.Value,
                        Text = item.Text,
                        Selected = selectedTags?.Contains(item.Value, StringComparison.CurrentCultureIgnoreCase) ?? false
                    }
        );
    }
}
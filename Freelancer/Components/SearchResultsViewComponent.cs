using Freelancer.Models.Search;

using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Components;

[ViewComponent(Name = "SearchResults")]
public class SearchResultsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SearchResponseModel model)
    {
        return View(model);
    }
}
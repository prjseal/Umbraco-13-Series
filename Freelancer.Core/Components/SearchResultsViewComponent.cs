using Freelancer.Core.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Core.Components;

[ViewComponent(Name = "SearchResults")]
public class SearchResultsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SearchResponseModel model)
    {
        return View(model);
    }
}
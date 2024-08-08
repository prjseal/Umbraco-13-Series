using Freelancer.Core.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Core.Components;

[ViewComponent(Name = "SearchForm")]
public class SearchFormViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SearchRequestModel model)
    {
        return View(model);
    }
}
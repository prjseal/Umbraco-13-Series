using Freelancer.Models.Search;
using Freelancer.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Components;

[ViewComponent(Name = "Pagination")]
public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationViewModel model)
    {
        model ??= new PaginationViewModel();

        return View(model);
    }
}
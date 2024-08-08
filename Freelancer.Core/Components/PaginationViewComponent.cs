using Freelancer.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Core.Components;

[ViewComponent(Name = "Pagination")]
public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationViewModel model)
    {
        model ??= new PaginationViewModel();

        return View(model);
    }
}
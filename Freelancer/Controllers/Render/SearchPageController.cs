using Freelancer.Configuration;
using Freelancer.Extensions;
using Freelancer.Helpers;
using Freelancer.Models.ContentModels;
using Freelancer.Models.Search;
using Freelancer.Models.ViewModels;
using Freelancer.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;

using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Freelancer.Controllers.Render;

public class SearchPageController(
    ILogger<RenderController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor,
    IHttpContextAccessor httpContextAccessor,
    ISearchService searchService,
    IOptions<FreelancerConfig> freelancerConfig)
        : RenderController(logger, compositeViewEngine, umbracoContextAccessor)
{
    private readonly FreelancerConfig _freelancerConfig = freelancerConfig.Value;

    public override IActionResult Index()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var query = httpContext?.Request.Query[Constants.QueryStrings.Query];
        var page = httpContext?.Request.Query[Constants.QueryStrings.Page];
        var tags = httpContext?.Request.Query[Constants.QueryStrings.Tags];

        if (CurrentPage == null) return BadRequest();

        var allTags = CurrentPage.GetPageTagsSelectList();

        var pageSize = _freelancerConfig?.SearchSettings?.PageSize ?? Constants.Search.DefaultPageSize;

        var searchRequest = new SearchRequestModel(query, page, pageSize, tags, allTags);

        var searchResponse = searchService.Search(searchRequest);

        var pagination = new PaginationViewModel
        {
            TotalResults = searchResponse.TotalResultCount,
            TotalPages = (int)Math.Ceiling((double)(searchResponse.TotalResultCount / searchRequest.PageSize)),
            ResultsPerPage = searchRequest.PageSize,
            CurrentPage = searchRequest.Page,
            PaginationUrlFormat = PaginationHelper.GetPaginationUrlFormat(Request.Path, Request?.QueryString.ToString(), page)
        };

        var model = new SearchPageContentModel(CurrentPage)
        {
            SearchRequest = searchRequest,
            SearchResponse = searchResponse,
            Pagination = pagination
        };

        return CurrentTemplate(model);
    }
}
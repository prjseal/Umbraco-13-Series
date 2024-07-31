using Examine;
using Examine.Search;

using Freelancer.Models.Search;

using Lucene.Net.Analysis.Core;

using StackExchange.Profiling.Internal;

using Umbraco.Cms.Infrastructure.Examine;

using UmbracoConstants = Umbraco.Cms.Core.Constants;

namespace Freelancer.Services;

public class SearchService : ISearchService
{
    private readonly IExamineManager _examineManager;
    private readonly string[] _docTypesToExclude =
        [PageTags.ModelTypeAlias, PageTag.ModelTypeAlias, SiteSettings.ModelTypeAlias, ReusableContentRepository.ModelTypeAlias, ReusableContent.ModelTypeAlias];

    public SearchService(IExamineManager examineManager)
    {
        _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));

    }

    public SearchResponseModel Search(SearchRequestModel searchRequest)
    {
        if (searchRequest == null || !_examineManager.TryGetIndex(UmbracoConstants.UmbracoIndexes.ExternalIndexName, out IIndex? index))
        {
            return new SearchResponseModel();
        }

        IBooleanOperation? query = index.Searcher.CreateQuery(IndexTypes.Content)
            .GroupedNot(["umbracoNaviHide"], ["1"])
            .And().GroupedNot(["__NodeTpeAlias"], _docTypesToExclude);

        string[]? terms = !string.IsNullOrWhiteSpace(searchRequest.Query)
            ? searchRequest.Query.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(x.ToLower()) && x.Length > 2).ToArray() : null;

        if (terms != null && terms.Length > 0)
        {
            query!.And().Group(q => q
                .GroupedOr(["nodeName"], terms), BooleanOperation.Or);
        }

        ISearchResults? pageOfResults = query.Execute(new QueryOptions(searchRequest.Skip, searchRequest.PageSize));

        return new SearchResponseModel(searchRequest.Query, pageOfResults.TotalItemCount, pageOfResults);
    }
}
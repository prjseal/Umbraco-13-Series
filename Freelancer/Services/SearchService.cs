using Examine;
using Examine.Search;

using Freelancer.Extensions;
using Freelancer.Models.Search;

using Lucene.Net.Analysis.Core;

using StackExchange.Profiling.Internal;

using Umbraco.Cms.Infrastructure.Examine;

namespace Freelancer.Services;

public class SearchService : ISearchService
{
    private readonly IExamineManager _examineManager;
    private readonly string[] _docTypesToExclude =
        [PageTags.ModelTypeAlias,
            PageTag.ModelTypeAlias,
            SiteSettings.ModelTypeAlias,
            ReusableContentRepository.ModelTypeAlias,
            ReusableContentItem.ModelTypeAlias];

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
            .And().GroupedNot(["__NodeTypeAlias"], _docTypesToExclude);

        string[]? terms = !string.IsNullOrWhiteSpace(searchRequest.Query)
            ? searchRequest.Query.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(x.ToLower()) && x.Length > 2).ToArray() : null;

        if (terms != null && terms.Length > 0)
        {
            query!.And().Group(q => q
                .GroupedOr(["metaTitle"], terms.Boost(80))
                .Or()
                .GroupedOr(["nodeName"], terms.Boost(70))
                .Or()
                .GroupedOr(["metaTitle"], terms.Fuzzy())
                .Or()
                .GroupedOr(["metaTitle"], terms.MultipleCharacterWildcard())
                .Or()
                .GroupedOr(["nodeName"], terms.Fuzzy())
                .Or()
                .GroupedOr(["nodeName"], terms.MultipleCharacterWildcard())
                .Or()
                .GroupedOr(["metaDescription"], terms.Boost(50))
                .Or()
                .GroupedOr(["headerContent"], terms.Boost(40))
                .Or()
                .GroupedOr(["mainContent"], terms.Boost(40)

                ), BooleanOperation.Or);
        }

        if (searchRequest.SelectedTags != null)
        {
            query.And().GroupedOr(["tags"], searchRequest.SelectedTags);
        }

        ISearchResults? pageOfResults = query.Execute(new QueryOptions(searchRequest.Skip, searchRequest.PageSize));

        return new SearchResponseModel(searchRequest.Query, pageOfResults.TotalItemCount, pageOfResults);
    }
}
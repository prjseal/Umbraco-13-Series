using Examine;

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Web;

namespace Freelancer.Search;

public class IndexComponent(IExamineManager examineManager, IUmbracoContextFactory umbracoContextFactory) : IComponent
{
    public void Initialize()
    {
        if (!examineManager.TryGetIndex(UmbracoConstants.UmbracoIndexes.ExternalIndexName, out IIndex index))
        {
            throw new InvalidOperationException($"No index found by name {UmbracoConstants.UmbracoIndexes.ExternalIndexName}");
        }

        index.TransformingIndexValues += UmbracoContextIndex_TransforminIndexValues;
    }

    public void Terminate()
    { }

    private void UmbracoContextIndex_TransforminIndexValues(object? sender, IndexingItemEventArgs e)
    {
        if (int.TryParse(e.ValueSet.Id, out var nodeId))
        {
            var values = e.ValueSet.Values.ToDictionary(x => x.Key, x => x.Value.ToList());
            if (values.TryGetValue("pageTags", out var pageTags))
            {
                using var umbracoContext = umbracoContextFactory.EnsureUmbracoContext();

                var contentNode = umbracoContext?.UmbracoContext?.Content?.GetById(nodeId);

                if (contentNode is not ITaggingProperties tagging) return;

                var tags = tagging.PageTags;
                if (tags == null || !tags.Any()) return;

                if (!values.TryGetValue("tags", out var value))
                {
                    value = [];
                }

                foreach (PageTag tag in tags.OfType<PageTag>())
                {
                    if (string.IsNullOrWhiteSpace(tag?.TagAlias)) continue;
                    value.Add(tag.TagAlias);
                }

                values["tags"] = value;
            }
            e.SetValues(values.ToDictionary(x => x.Key, x => (IEnumerable<object>)x.Value));
        }
    }
}
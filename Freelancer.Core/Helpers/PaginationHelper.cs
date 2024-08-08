using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Freelancer.Core.Helpers;

public static class PaginationHelper
{
    public static string? GetPaginationUrlFormat(PathString path, string? queryString, string? page)
    {
        var nameValues = QueryHelpers.ParseQuery(queryString);

        if (!string.IsNullOrWhiteSpace(page))
        {
            nameValues.Remove(Constants.QueryStrings.Page);
            nameValues.Add(Constants.QueryStrings.Page, "{0}");
        }
        else
        {
            nameValues.Add(Constants.QueryStrings.Page, "{0}");
        }

        return WebUtility.UrlDecode(QueryHelpers.AddQueryString(path, nameValues));
    }
}
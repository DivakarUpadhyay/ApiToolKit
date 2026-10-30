using ApiToolKit.Application.DTOs.SearchConsole.SearchAnalytics;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

namespace ApiToolKit.Application.Services.SearchConsole;

public class SearchConsoleSearchAnalyticsService : SearchConsoleServiceBase, ISearchConsoleSearchAnalyticsService
{
    public SearchConsoleSearchAnalyticsService(ISearchConsoleApiClient apiClient, ISearchConsoleAuthService authService, ISearchConsoleAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> QuerySearchAnalyticsAsync(QuerySearchAnalyticsRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/searchAnalytics/query";
        return await SendAsync(r.AccountKey, "QuerySearchAnalytics", HttpMethod.Post, url, null, new { startDate = r.StartDate, endDate = r.EndDate, dimensions = r.Dimensions, type = r.Type, aggregationType = r.AggregationType, rowLimit = r.RowLimit, startRow = r.StartRow, dataState = r.DataState, dimensionFilterGroups = r.DimensionFilterGroups });
    }

    public async Task<Result<object>> GetKeywordRankingsAsync(GetKeywordRankingsRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/searchAnalytics/query";
        return await SendAsync(r.AccountKey, "GetKeywordRankings", HttpMethod.Post, url, null, new { startDate = r.StartDate, endDate = r.EndDate, dimensions = new[] { "query" }, rowLimit = r.RowLimit ?? 100 });
    }

    public async Task<Result<object>> GetTopPagesAsync(GetTopPagesRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/searchAnalytics/query";
        return await SendAsync(r.AccountKey, "GetTopPages", HttpMethod.Post, url, null, new { startDate = r.StartDate, endDate = r.EndDate, dimensions = new[] { "page" }, rowLimit = r.RowLimit ?? 100 });
    }
}

using ApiToolKit.Application.DTOs.SearchConsole.SearchAnalytics;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleSearchAnalyticsService
{
    Task<Result<object>> QuerySearchAnalyticsAsync(QuerySearchAnalyticsRequest request);
    Task<Result<object>> GetKeywordRankingsAsync(GetKeywordRankingsRequest request);
    Task<Result<object>> GetTopPagesAsync(GetTopPagesRequest request);
}

using ApiToolKit.Application.DTOs.BusinessProfile.Performance;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfilePerformanceService : BusinessProfileServiceBase, IBusinessProfilePerformanceService
{
    public BusinessProfilePerformanceService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetDailyMetricsAsync(GetDailyMetricsRequest r)
    {
        var url = $"https://businessprofileperformance.googleapis.com/v1/locations/{r.LocationId}:fetchMultiDailyMetricsTimeSeries";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "dailyMetrics", r.DailyMetrics);
        Add(query, "dailyRange.startDate.year", DatePart(r.StartDate, 0));
        Add(query, "dailyRange.startDate.month", DatePart(r.StartDate, 1));
        Add(query, "dailyRange.startDate.day", DatePart(r.StartDate, 2));
        Add(query, "dailyRange.endDate.year", DatePart(r.EndDate, 0));
        Add(query, "dailyRange.endDate.month", DatePart(r.EndDate, 1));
        Add(query, "dailyRange.endDate.day", DatePart(r.EndDate, 2));
        return await SendAsync(r.AccountKey, "GetDailyMetrics", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetSearchKeywordImpressionsAsync(GetSearchKeywordImpressionsRequest r)
    {
        var url = $"https://businessprofileperformance.googleapis.com/v1/locations/{r.LocationId}/searchkeywords/impressions/monthly";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "monthlyRange.startMonth.year", DatePart(r.StartMonth, 0));
        Add(query, "monthlyRange.startMonth.month", DatePart(r.StartMonth, 1));
        Add(query, "monthlyRange.endMonth.year", DatePart(r.EndMonth, 0));
        Add(query, "monthlyRange.endMonth.month", DatePart(r.EndMonth, 1));
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "GetSearchKeywordImpressions", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetDailyMetricTimeSeriesAsync(GetDailyMetricTimeSeriesRequest r)
    {
        var url = $"https://businessprofileperformance.googleapis.com/v1/locations/{r.LocationId}:getDailyMetricsTimeSeries";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dailyMetric", r.DailyMetric);
        Add(query, "dailyRange.startDate.year", DatePart(r.StartDate, 0));
        Add(query, "dailyRange.startDate.month", DatePart(r.StartDate, 1));
        Add(query, "dailyRange.startDate.day", DatePart(r.StartDate, 2));
        Add(query, "dailyRange.endDate.year", DatePart(r.EndDate, 0));
        Add(query, "dailyRange.endDate.month", DatePart(r.EndDate, 1));
        Add(query, "dailyRange.endDate.day", DatePart(r.EndDate, 2));
        return await SendAsync(r.AccountKey, "GetDailyMetricTimeSeries", HttpMethod.Get, url, query);
    }
}

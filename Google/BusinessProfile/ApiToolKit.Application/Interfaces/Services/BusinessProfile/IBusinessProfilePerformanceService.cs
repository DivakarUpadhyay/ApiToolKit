using ApiToolKit.Application.DTOs.BusinessProfile.Performance;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfilePerformanceService
{
    Task<Result<object>> GetDailyMetricsAsync(GetDailyMetricsRequest request);
    Task<Result<object>> GetSearchKeywordImpressionsAsync(GetSearchKeywordImpressionsRequest request);
    Task<Result<object>> GetDailyMetricTimeSeriesAsync(GetDailyMetricTimeSeriesRequest request);
}

using ApiToolKit.Application.DTOs.Analytics.Realtime;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsRealtimeService
{
    Task<Result<RunRealtimeReportResponse>> RunRealtimeReportAsync(RunRealtimeReportRequest request, string accessToken);
}

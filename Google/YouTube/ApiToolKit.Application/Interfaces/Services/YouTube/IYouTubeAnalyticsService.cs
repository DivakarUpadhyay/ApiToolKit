using ApiToolKit.Application.DTOs.YouTube.Analytics;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeAnalyticsService
{
    Task<Result<AnalyticsReportResponse>> RunReportAsync(RunAnalyticsReportRequest request, string accessToken);
}

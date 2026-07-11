using ApiToolKit.Application.DTOs.Analytics.FunnelReport;
using ApiToolKit.Application.DTOs.Analytics.PivotReport;
using ApiToolKit.Application.DTOs.Analytics.Reports;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsReportService
{
    Task<Result<RunReportResponse>> RunReportAsync(RunReportRequest request, string accessToken);
    Task<Result<BatchRunReportsResponse>> BatchRunReportsAsync(BatchRunReportsRequest request, string accessToken);
    Task<Result<RunPivotReportResponse>> RunPivotReportAsync(RunPivotReportRequest request, string accessToken);
    Task<Result<RunFunnelReportResponse>> RunFunnelReportAsync(RunFunnelReportRequest request, string accessToken);
}

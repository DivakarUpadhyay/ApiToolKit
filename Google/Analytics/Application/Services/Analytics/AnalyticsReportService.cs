using ApiToolKit.Application.DTOs.Analytics.FunnelReport;
using ApiToolKit.Application.DTOs.Analytics.PivotReport;
using ApiToolKit.Application.DTOs.Analytics.Reports;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsReportService : IAnalyticsReportService
{
    private readonly IAnalyticsApiClient _apiClient;
    private readonly IAnalyticsReportRepository _reportRepo;
    private readonly IAnalyticsAuthRepository _authRepo;
    private const string DataApiBase = "https://analyticsdata.googleapis.com/v1beta/";

    public AnalyticsReportService(
        IAnalyticsApiClient apiClient,
        IAnalyticsReportRepository reportRepo,
        IAnalyticsAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _reportRepo = reportRepo;
        _authRepo = authRepo;
    }

    public async Task<Result<RunReportResponse>> RunReportAsync(RunReportRequest request, string accessToken)
    {
        var url = $"{DataApiBase}properties/{request.PropertyId}:runReport";
        var response = await _apiClient.PostAsync<RunReportResponse>(url, request, accessToken);

        if (response == null)
            return Result<RunReportResponse>.Fail("No response from Google Analytics Data API.");

        await LogAndSaveReportAsync(request.PropertyId, "RunReport", request, response);
        return Result<RunReportResponse>.Ok(response);
    }

    public async Task<Result<BatchRunReportsResponse>> BatchRunReportsAsync(BatchRunReportsRequest request, string accessToken)
    {
        var url = $"{DataApiBase}properties/{request.PropertyId}:batchRunReports";
        var response = await _apiClient.PostAsync<BatchRunReportsResponse>(url, request, accessToken);

        if (response == null)
            return Result<BatchRunReportsResponse>.Fail("No response from Google Analytics Data API.");

        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = request.PropertyId,
            ReportType = "BatchRunReports",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<BatchRunReportsResponse>.Ok(response);
    }

    public async Task<Result<RunPivotReportResponse>> RunPivotReportAsync(RunPivotReportRequest request, string accessToken)
    {
        var url = $"{DataApiBase}properties/{request.PropertyId}:runPivotReport";
        var response = await _apiClient.PostAsync<RunPivotReportResponse>(url, request, accessToken);

        if (response == null)
            return Result<RunPivotReportResponse>.Fail("No response from Google Analytics Data API.");

        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = request.PropertyId,
            ReportType = "RunPivotReport",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<RunPivotReportResponse>.Ok(response);
    }

    public async Task<Result<RunFunnelReportResponse>> RunFunnelReportAsync(RunFunnelReportRequest request, string accessToken)
    {
        var url = $"{DataApiBase}properties/{request.PropertyId}:runFunnelReport";
        var response = await _apiClient.PostAsync<RunFunnelReportResponse>(url, request, accessToken);

        if (response == null)
            return Result<RunFunnelReportResponse>.Fail("No response from Google Analytics Data API.");

        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = request.PropertyId,
            ReportType = "RunFunnelReport",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<RunFunnelReportResponse>.Ok(response);
    }

    private async Task LogAndSaveReportAsync(string propertyId, string reportType, object request, RunReportResponse response)
    {
        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = propertyId,
            ReportType = reportType,
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        await _reportRepo.SaveReportAsync(new AnalyticsSavedReport
        {
            PropertyId = propertyId,
            ReportName = reportType,
            Query = JsonConvert.SerializeObject(request),
            ResponseJson = JsonConvert.SerializeObject(response),
            RowCount = response.RowCount,
            CreatedAt = DateTime.UtcNow
        });
    }
}

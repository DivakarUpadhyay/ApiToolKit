using ApiToolKit.Application.DTOs.Analytics.Realtime;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsRealtimeService : IAnalyticsRealtimeService
{
    private readonly IAnalyticsApiClient _apiClient;
    private readonly IAnalyticsAuthRepository _authRepo;
    private const string DataApiBase = "https://analyticsdata.googleapis.com/v1beta/";

    public AnalyticsRealtimeService(IAnalyticsApiClient apiClient, IAnalyticsAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public async Task<Result<RunRealtimeReportResponse>> RunRealtimeReportAsync(RunRealtimeReportRequest request, string accessToken)
    {
        var url = $"{DataApiBase}properties/{request.PropertyId}:runRealtimeReport";
        var response = await _apiClient.PostAsync<RunRealtimeReportResponse>(url, request, accessToken);

        if (response == null)
            return Result<RunRealtimeReportResponse>.Fail("No response from Google Analytics Realtime API.");

        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = request.PropertyId,
            ReportType = "RunRealtimeReport",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<RunRealtimeReportResponse>.Ok(response);
    }
}

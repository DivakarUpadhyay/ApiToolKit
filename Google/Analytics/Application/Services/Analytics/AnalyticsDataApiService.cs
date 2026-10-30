using ApiToolKit.Application.DTOs.Analytics.DataApi;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsDataApiService : IAnalyticsDataApiService
{
    private readonly IAnalyticsApiClient _apiClient;
    private readonly IAnalyticsAuthRepository _authRepo;
    private const string BetaBase = "https://analyticsdata.googleapis.com/v1beta/properties/";
    private const string AlphaBase = "https://analyticsdata.googleapis.com/v1alpha/properties/";

    public AnalyticsDataApiService(IAnalyticsApiClient apiClient, IAnalyticsAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Task<Result<JsonNode>> BatchRunPivotReportsAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "BatchRunPivotReports", HttpMethod.Post, $"{BetaBase}{request.PropertyId}:batchRunPivotReports", request.Body, accessToken);

    public Task<Result<JsonNode>> CheckCompatibilityAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "CheckCompatibility", HttpMethod.Post, $"{BetaBase}{request.PropertyId}:checkCompatibility", request.Body, accessToken);

    public Task<Result<JsonNode>> ListAudienceExportsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken) =>
        SendAsync(propertyId, "ListAudienceExports", HttpMethod.Get, Paged($"{BetaBase}{propertyId}/audienceExports", pageSize, pageToken), null, accessToken);

    public Task<Result<JsonNode>> GetAudienceExportAsync(string propertyId, string audienceExportId, string accessToken) =>
        SendAsync(propertyId, "GetAudienceExport", HttpMethod.Get, $"{BetaBase}{propertyId}/audienceExports/{audienceExportId}", null, accessToken);

    public Task<Result<JsonNode>> CreateAudienceExportAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "CreateAudienceExport", HttpMethod.Post, $"{BetaBase}{request.PropertyId}/audienceExports", request.Body, accessToken);

    public Task<Result<JsonNode>> QueryAudienceExportAsync(PropertyResourceBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "QueryAudienceExport", HttpMethod.Post,
            $"{BetaBase}{request.PropertyId}/audienceExports/{request.ResourceId}:query", request.Body ?? new JsonObject(), accessToken);

    public Task<Result<JsonNode>> GetPropertyQuotasSnapshotAsync(string propertyId, string accessToken) =>
        SendAsync(propertyId, "GetPropertyQuotasSnapshot", HttpMethod.Get, $"{AlphaBase}{propertyId}/propertyQuotasSnapshot", null, accessToken);

    public Task<Result<JsonNode>> ListAudienceListsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken) =>
        SendAsync(propertyId, "ListAudienceLists", HttpMethod.Get, Paged($"{AlphaBase}{propertyId}/audienceLists", pageSize, pageToken), null, accessToken);

    public Task<Result<JsonNode>> GetAudienceListAsync(string propertyId, string audienceListId, string accessToken) =>
        SendAsync(propertyId, "GetAudienceList", HttpMethod.Get, $"{AlphaBase}{propertyId}/audienceLists/{audienceListId}", null, accessToken);

    public Task<Result<JsonNode>> CreateAudienceListAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "CreateAudienceList", HttpMethod.Post, $"{AlphaBase}{request.PropertyId}/audienceLists", request.Body, accessToken);

    public Task<Result<JsonNode>> QueryAudienceListAsync(PropertyResourceBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "QueryAudienceList", HttpMethod.Post,
            $"{AlphaBase}{request.PropertyId}/audienceLists/{request.ResourceId}:query", request.Body ?? new JsonObject(), accessToken);

    public Task<Result<JsonNode>> ListRecurringAudienceListsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken) =>
        SendAsync(propertyId, "ListRecurringAudienceLists", HttpMethod.Get, Paged($"{AlphaBase}{propertyId}/recurringAudienceLists", pageSize, pageToken), null, accessToken);

    public Task<Result<JsonNode>> GetRecurringAudienceListAsync(string propertyId, string recurringAudienceListId, string accessToken) =>
        SendAsync(propertyId, "GetRecurringAudienceList", HttpMethod.Get, $"{AlphaBase}{propertyId}/recurringAudienceLists/{recurringAudienceListId}", null, accessToken);

    public Task<Result<JsonNode>> CreateRecurringAudienceListAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "CreateRecurringAudienceList", HttpMethod.Post, $"{AlphaBase}{request.PropertyId}/recurringAudienceLists", request.Body, accessToken);

    public Task<Result<JsonNode>> ListReportTasksAsync(string propertyId, int? pageSize, string? pageToken, string accessToken) =>
        SendAsync(propertyId, "ListReportTasks", HttpMethod.Get, Paged($"{AlphaBase}{propertyId}/reportTasks", pageSize, pageToken), null, accessToken);

    public Task<Result<JsonNode>> GetReportTaskAsync(string propertyId, string reportTaskId, string accessToken) =>
        SendAsync(propertyId, "GetReportTask", HttpMethod.Get, $"{AlphaBase}{propertyId}/reportTasks/{reportTaskId}", null, accessToken);

    public Task<Result<JsonNode>> CreateReportTaskAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "CreateReportTask", HttpMethod.Post, $"{AlphaBase}{request.PropertyId}/reportTasks", request.Body, accessToken);

    public Task<Result<JsonNode>> QueryReportTaskAsync(PropertyResourceBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "QueryReportTask", HttpMethod.Post,
            $"{AlphaBase}{request.PropertyId}/reportTasks/{request.ResourceId}:query", request.Body ?? new JsonObject(), accessToken);

    public Task<Result<JsonNode>> ChatAsync(PropertyBodyRequest request, string accessToken) =>
        SendAsync(request.PropertyId, "Chat", HttpMethod.Post, $"{AlphaBase}{request.PropertyId}:chat", request.Body, accessToken);

    private static string Paged(string url, int? pageSize, string? pageToken)
    {
        var query = new List<string>();
        if (pageSize.HasValue) query.Add($"pageSize={pageSize.Value}");
        if (!string.IsNullOrEmpty(pageToken)) query.Add($"pageToken={Uri.EscapeDataString(pageToken)}");
        return query.Count == 0 ? url : $"{url}?{string.Join("&", query)}";
    }

    private async Task<Result<JsonNode>> SendAsync(string propertyId, string reportType, HttpMethod method, string url, JsonNode? body, string accessToken)
    {
        var requestJson = body?.ToJsonString();
        var (statusCode, responseText) = await _apiClient.SendRawAsync(method, url, requestJson, accessToken);
        var isSuccess = statusCode is >= 200 and < 300;

        await _authRepo.SaveApiLogAsync(new AnalyticsApiLog
        {
            PropertyId = propertyId,
            ReportType = reportType,
            RequestBody = requestJson,
            ResponseBody = responseText,
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });

        JsonNode? parsed;
        try { parsed = string.IsNullOrWhiteSpace(responseText) ? new JsonObject() : JsonNode.Parse(responseText); }
        catch (JsonException) { parsed = JsonValue.Create(responseText); }

        if (!isSuccess)
            return Result<JsonNode>.Fail((parsed as JsonObject)?["error"]?["message"]?.ToString() ??$"Google Analytics Data API returned HTTP {statusCode}.", statusCode);

        return Result<JsonNode>.Ok(parsed ?? new JsonObject());
    }
}

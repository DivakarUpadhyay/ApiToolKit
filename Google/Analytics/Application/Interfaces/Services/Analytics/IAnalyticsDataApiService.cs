using ApiToolKit.Application.DTOs.Analytics.DataApi;
using ApiToolKit.Domain.Common;
using System.Text.Json.Nodes;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

/// <summary>
/// Remaining Google Analytics Data API methods (v1beta and v1alpha) not covered by the report services.
/// </summary>
public interface IAnalyticsDataApiService
{
    Task<Result<JsonNode>> BatchRunPivotReportsAsync(PropertyBodyRequest request, string accessToken);
    Task<Result<JsonNode>> CheckCompatibilityAsync(PropertyBodyRequest request, string accessToken);

    Task<Result<JsonNode>> ListAudienceExportsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken);
    Task<Result<JsonNode>> GetAudienceExportAsync(string propertyId, string audienceExportId, string accessToken);
    Task<Result<JsonNode>> CreateAudienceExportAsync(PropertyBodyRequest request, string accessToken);
    Task<Result<JsonNode>> QueryAudienceExportAsync(PropertyResourceBodyRequest request, string accessToken);

    Task<Result<JsonNode>> GetPropertyQuotasSnapshotAsync(string propertyId, string accessToken);
    Task<Result<JsonNode>> ListAudienceListsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken);
    Task<Result<JsonNode>> GetAudienceListAsync(string propertyId, string audienceListId, string accessToken);
    Task<Result<JsonNode>> CreateAudienceListAsync(PropertyBodyRequest request, string accessToken);
    Task<Result<JsonNode>> QueryAudienceListAsync(PropertyResourceBodyRequest request, string accessToken);
    Task<Result<JsonNode>> ListRecurringAudienceListsAsync(string propertyId, int? pageSize, string? pageToken, string accessToken);
    Task<Result<JsonNode>> GetRecurringAudienceListAsync(string propertyId, string recurringAudienceListId, string accessToken);
    Task<Result<JsonNode>> CreateRecurringAudienceListAsync(PropertyBodyRequest request, string accessToken);
    Task<Result<JsonNode>> ListReportTasksAsync(string propertyId, int? pageSize, string? pageToken, string accessToken);
    Task<Result<JsonNode>> GetReportTaskAsync(string propertyId, string reportTaskId, string accessToken);
    Task<Result<JsonNode>> CreateReportTaskAsync(PropertyBodyRequest request, string accessToken);
    Task<Result<JsonNode>> QueryReportTaskAsync(PropertyResourceBodyRequest request, string accessToken);
    Task<Result<JsonNode>> ChatAsync(PropertyBodyRequest request, string accessToken);
}

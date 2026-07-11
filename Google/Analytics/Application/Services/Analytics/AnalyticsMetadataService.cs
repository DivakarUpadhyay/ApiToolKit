using ApiToolKit.Application.DTOs.Analytics.Metadata;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsMetadataService : IAnalyticsMetadataService
{
    private readonly IAnalyticsApiClient _apiClient;
    private const string DataApiBase = "https://analyticsdata.googleapis.com/v1beta/";

    public AnalyticsMetadataService(IAnalyticsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<MetadataResponse>> GetMetadataAsync(string propertyId, string accessToken)
    {
        var url = $"{DataApiBase}properties/{propertyId}/metadata";
        var response = await _apiClient.GetAsync<MetadataResponse>(url, accessToken);

        if (response == null)
            return Result<MetadataResponse>.Fail("No response from Google Analytics Metadata API.");

        return Result<MetadataResponse>.Ok(response);
    }
}

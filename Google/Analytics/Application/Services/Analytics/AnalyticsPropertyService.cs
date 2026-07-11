using ApiToolKit.Application.DTOs.Analytics.Property;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsPropertyService : IAnalyticsPropertyService
{
    private readonly IAnalyticsApiClient _apiClient;
    private const string AdminApiBase = "https://analyticsadmin.googleapis.com/v1beta/";

    public AnalyticsPropertyService(IAnalyticsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<ListAccountsResponse>> ListAccountsAsync(string accessToken)
    {
        var url = $"{AdminApiBase}accounts";
        var response = await _apiClient.GetAsync<ListAccountsResponse>(url, accessToken);

        if (response == null)
            return Result<ListAccountsResponse>.Fail("No response from Google Analytics Admin API.");

        return Result<ListAccountsResponse>.Ok(response);
    }

    public async Task<Result<ListPropertiesResponse>> ListPropertiesAsync(string accountName, string accessToken)
    {
        var encodedFilter = Uri.EscapeDataString($"parent:{accountName}");
        var url = $"{AdminApiBase}properties?filter={encodedFilter}";
        var response = await _apiClient.GetAsync<ListPropertiesResponse>(url, accessToken);

        if (response == null)
            return Result<ListPropertiesResponse>.Fail("No response from Google Analytics Admin API.");

        return Result<ListPropertiesResponse>.Ok(response);
    }

    public async Task<Result<PropertyResponse>> GetPropertyAsync(string propertyName, string accessToken)
    {
        var url = $"{AdminApiBase}{propertyName}";
        var response = await _apiClient.GetAsync<PropertyResponse>(url, accessToken);

        if (response == null)
            return Result<PropertyResponse>.Fail("No response from Google Analytics Admin API.");

        return Result<PropertyResponse>.Ok(response);
    }

    public async Task<Result<ListDataStreamsResponse>> ListDataStreamsAsync(string propertyId, string accessToken)
    {
        var url = $"{AdminApiBase}properties/{propertyId}/dataStreams";
        var response = await _apiClient.GetAsync<ListDataStreamsResponse>(url, accessToken);

        if (response == null)
            return Result<ListDataStreamsResponse>.Fail("No response from Google Analytics Admin API.");

        return Result<ListDataStreamsResponse>.Ok(response);
    }
}

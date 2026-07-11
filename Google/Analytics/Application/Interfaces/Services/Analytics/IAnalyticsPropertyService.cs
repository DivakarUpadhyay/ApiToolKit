using ApiToolKit.Application.DTOs.Analytics.Property;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsPropertyService
{
    Task<Result<ListAccountsResponse>> ListAccountsAsync(string accessToken);
    Task<Result<ListPropertiesResponse>> ListPropertiesAsync(string accountName, string accessToken);
    Task<Result<PropertyResponse>> GetPropertyAsync(string propertyName, string accessToken);
    Task<Result<ListDataStreamsResponse>> ListDataStreamsAsync(string propertyId, string accessToken);
}

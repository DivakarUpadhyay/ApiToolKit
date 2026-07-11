using ApiToolKit.Application.DTOs.Analytics.Metadata;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsMetadataService
{
    Task<Result<MetadataResponse>> GetMetadataAsync(string propertyId, string accessToken);
}

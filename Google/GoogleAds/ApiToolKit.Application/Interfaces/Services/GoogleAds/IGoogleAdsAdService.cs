using ApiToolKit.Application.DTOs.GoogleAds.Ad;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsAdService
{
    Task<Result<AdListResponse>> ListAdsAsync(ListAdsRequest request);
    Task<Result<MutateOperationResponse>> CreateResponsiveSearchAdAsync(CreateResponsiveSearchAdRequest request);
    Task<Result<MutateOperationResponse>> UpdateAdAsync(UpdateAdRequest request);
    Task<Result<bool>> RemoveAdAsync(string customerId, string resourceName);
}

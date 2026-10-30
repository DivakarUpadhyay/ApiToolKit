using ApiToolKit.Application.DTOs.BusinessProfile.Location;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileLocationService
{
    Task<Result<object>> ListLocationsAsync(ListLocationsRequest request);
    Task<Result<object>> GetLocationAsync(GetLocationRequest request);
    Task<Result<object>> CreateLocationAsync(CreateLocationRequest request);
    Task<Result<object>> UpdateLocationAsync(UpdateLocationRequest request);
    Task<Result<object>> DeleteLocationAsync(DeleteLocationRequest request);
    Task<Result<object>> GetLocationAttributesAsync(GetLocationAttributesRequest request);
    Task<Result<object>> GetGoogleUpdatedLocationAsync(GetGoogleUpdatedLocationRequest request);
    Task<Result<object>> UpdateLocationAttributesAsync(UpdateLocationAttributesRequest request);
    Task<Result<object>> GetGoogleUpdatedAttributesAsync(GetGoogleUpdatedAttributesRequest request);
    Task<Result<object>> TransferLocationAsync(TransferLocationRequest request);
    Task<Result<object>> ListLocationAdminsAsync(ListLocationAdminsRequest request);
    Task<Result<object>> InviteLocationAdminAsync(InviteLocationAdminRequest request);
    Task<Result<object>> UpdateLocationAdminAsync(UpdateLocationAdminRequest request);
    Task<Result<object>> RemoveLocationAdminAsync(RemoveLocationAdminRequest request);
    Task<Result<object>> SearchGoogleLocationsAsync(SearchGoogleLocationsRequest request);
    Task<Result<object>> GetFollowersMetadataAsync(GetFollowersMetadataRequest request);
    Task<Result<object>> GetFoodMenusAsync(GetFoodMenusRequest request);
    Task<Result<object>> UpdateFoodMenusAsync(UpdateFoodMenusRequest request);
    Task<Result<object>> GetServiceListAsync(GetServiceListRequest request);
    Task<Result<object>> UpdateServiceListAsync(UpdateServiceListRequest request);
    Task<Result<object>> GetHealthProviderAttributesAsync(GetHealthProviderAttributesRequest request);
    Task<Result<object>> UpdateHealthProviderAttributesAsync(UpdateHealthProviderAttributesRequest request);
    Task<Result<object>> ListInsuranceNetworksAsync(ListInsuranceNetworksRequest request);
    Task<Result<object>> ListRecommendedGoogleLocationsAsync(ListRecommendedGoogleLocationsRequest request);
}

using ApiToolKit.Application.DTOs.GoogleAds.AdGroup;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsAdGroupService
{
    Task<Result<AdGroupListResponse>> ListAdGroupsAsync(ListAdGroupsRequest request);
    Task<Result<AdGroupResponse>> GetAdGroupAsync(string customerId, string resourceName);
    Task<Result<MutateOperationResponse>> CreateAdGroupAsync(CreateAdGroupRequest request);
    Task<Result<MutateOperationResponse>> UpdateAdGroupAsync(UpdateAdGroupRequest request);
    Task<Result<bool>> RemoveAdGroupAsync(string customerId, string resourceName);
}

using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAdvertiserGroupService
{
    Task<Result<object>> DeleteAdvertiserGroupAsync(DeleteAdvertiserGroupRequest request);
    Task<Result<object>> ListAdvertiserGroupsAsync(ListAdvertiserGroupsRequest request);
    Task<Result<object>> UpdateAdvertiserGroupAsync(UpdateAdvertiserGroupRequest request);
    Task<Result<object>> InsertAdvertiserGroupAsync(InsertAdvertiserGroupRequest request);
    Task<Result<object>> GetAdvertiserGroupAsync(GetAdvertiserGroupRequest request);
    Task<Result<object>> PatchAdvertiserGroupAsync(PatchAdvertiserGroupRequest request);
}

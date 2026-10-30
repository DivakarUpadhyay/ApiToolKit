using ApiToolKit.Application.DTOs.CampaignManager.CreativeGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCreativeGroupService
{
    Task<Result<object>> GetCreativeGroupAsync(GetCreativeGroupRequest request);
    Task<Result<object>> UpdateCreativeGroupAsync(UpdateCreativeGroupRequest request);
    Task<Result<object>> PatchCreativeGroupAsync(PatchCreativeGroupRequest request);
    Task<Result<object>> ListCreativeGroupsAsync(ListCreativeGroupsRequest request);
    Task<Result<object>> InsertCreativeGroupAsync(InsertCreativeGroupRequest request);
}

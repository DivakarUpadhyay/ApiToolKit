using ApiToolKit.Application.DTOs.CampaignManager.PlacementGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerPlacementGroupService
{
    Task<Result<object>> UpdatePlacementGroupAsync(UpdatePlacementGroupRequest request);
    Task<Result<object>> GetPlacementGroupAsync(GetPlacementGroupRequest request);
    Task<Result<object>> PatchPlacementGroupAsync(PatchPlacementGroupRequest request);
    Task<Result<object>> InsertPlacementGroupAsync(InsertPlacementGroupRequest request);
    Task<Result<object>> ListPlacementGroupsAsync(ListPlacementGroupsRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivityGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerFloodlightActivityGroupService
{
    Task<Result<object>> ListFloodlightActivityGroupsAsync(ListFloodlightActivityGroupsRequest request);
    Task<Result<object>> UpdateFloodlightActivityGroupAsync(UpdateFloodlightActivityGroupRequest request);
    Task<Result<object>> InsertFloodlightActivityGroupAsync(InsertFloodlightActivityGroupRequest request);
    Task<Result<object>> UpdateFloodlightActivityGroupAltAsync(UpdateFloodlightActivityGroupAltRequest request);
    Task<Result<object>> GetFloodlightActivityGroupAsync(GetFloodlightActivityGroupRequest request);
}

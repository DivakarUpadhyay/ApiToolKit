using ApiToolKit.Application.DTOs.CampaignManager.Placement;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerPlacementService
{
    Task<Result<object>> UpdatePlacementAsync(UpdatePlacementRequest request);
    Task<Result<object>> GetPlacementAsync(GetPlacementRequest request);
    Task<Result<object>> InsertPlacementAsync(InsertPlacementRequest request);
    Task<Result<object>> GeneratetagsPlacementAsync(GeneratetagsPlacementRequest request);
    Task<Result<object>> UpdatePlacementAltAsync(UpdatePlacementAltRequest request);
    Task<Result<object>> ListPlacementsAsync(ListPlacementsRequest request);
}

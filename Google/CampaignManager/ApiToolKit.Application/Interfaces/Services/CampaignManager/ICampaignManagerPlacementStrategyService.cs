using ApiToolKit.Application.DTOs.CampaignManager.PlacementStrategy;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerPlacementStrategyService
{
    Task<Result<object>> ListPlacementStrategiesAsync(ListPlacementStrategiesRequest request);
    Task<Result<object>> InsertPlacementStrategyAsync(InsertPlacementStrategyRequest request);
    Task<Result<object>> UpdatePlacementStrategyAsync(UpdatePlacementStrategyRequest request);
    Task<Result<object>> DeletePlacementStrategyAsync(DeletePlacementStrategyRequest request);
    Task<Result<object>> GetPlacementStrategyAsync(GetPlacementStrategyRequest request);
    Task<Result<object>> UpdatePlacementStrategyAltAsync(UpdatePlacementStrategyAltRequest request);
}

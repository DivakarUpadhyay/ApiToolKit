using ApiToolKit.Application.DTOs.CampaignManager.Region;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerRegionService
{
    Task<Result<object>> ListRegionsAsync(ListRegionsRequest request);
}

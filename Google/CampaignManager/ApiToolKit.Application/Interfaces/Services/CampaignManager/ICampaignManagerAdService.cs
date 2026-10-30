using ApiToolKit.Application.DTOs.CampaignManager.Ad;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAdService
{
    Task<Result<object>> UpdateAdAsync(UpdateAdRequest request);
    Task<Result<object>> ListAdsAsync(ListAdsRequest request);
    Task<Result<object>> GetAdAsync(GetAdRequest request);
    Task<Result<object>> UpdateAdAltAsync(UpdateAdAltRequest request);
    Task<Result<object>> InsertAdAsync(InsertAdRequest request);
}

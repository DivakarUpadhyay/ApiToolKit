using ApiToolKit.Application.DTOs.CampaignManager.Advertiser;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAdvertiserService
{
    Task<Result<object>> InsertAdvertiserAsync(InsertAdvertiserRequest request);
    Task<Result<object>> GetAdvertiserAsync(GetAdvertiserRequest request);
    Task<Result<object>> UpdateAdvertiserAsync(UpdateAdvertiserRequest request);
    Task<Result<object>> UpdateAdvertiserAltAsync(UpdateAdvertiserAltRequest request);
    Task<Result<object>> ListAdvertisersAsync(ListAdvertisersRequest request);
}

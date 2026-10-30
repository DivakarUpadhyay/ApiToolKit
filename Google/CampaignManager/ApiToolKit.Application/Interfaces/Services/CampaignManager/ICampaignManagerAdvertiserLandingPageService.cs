using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserLandingPage;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAdvertiserLandingPageService
{
    Task<Result<object>> GetAdvertiserLandingPageAsync(GetAdvertiserLandingPageRequest request);
    Task<Result<object>> UpdateAdvertiserLandingPageAsync(UpdateAdvertiserLandingPageRequest request);
    Task<Result<object>> InsertAdvertiserLandingPageAsync(InsertAdvertiserLandingPageRequest request);
    Task<Result<object>> UpdateAdvertiserLandingPageAltAsync(UpdateAdvertiserLandingPageAltRequest request);
    Task<Result<object>> ListAdvertiserLandingPagesAsync(ListAdvertiserLandingPagesRequest request);
}

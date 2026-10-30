using ApiToolKit.Application.DTOs.CampaignManager.MobileApp;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerMobileAppService
{
    Task<Result<object>> GetMobileAppAsync(GetMobileAppRequest request);
    Task<Result<object>> ListMobileAppsAsync(ListMobileAppsRequest request);
}

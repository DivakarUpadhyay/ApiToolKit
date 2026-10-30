using ApiToolKit.Application.DTOs.CampaignManager.BillingRate;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerBillingRateService
{
    Task<Result<object>> ListBillingRatesAsync(ListBillingRatesRequest request);
}

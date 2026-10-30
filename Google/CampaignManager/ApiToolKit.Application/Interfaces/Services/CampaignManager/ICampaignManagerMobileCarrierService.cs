using ApiToolKit.Application.DTOs.CampaignManager.MobileCarrier;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerMobileCarrierService
{
    Task<Result<object>> GetMobileCarrierAsync(GetMobileCarrierRequest request);
    Task<Result<object>> ListMobileCarriersAsync(ListMobileCarriersRequest request);
}

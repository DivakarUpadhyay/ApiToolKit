using ApiToolKit.Application.DTOs.CampaignManager.BillingProfile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerBillingProfileService
{
    Task<Result<object>> ListBillingProfilesAsync(ListBillingProfilesRequest request);
    Task<Result<object>> GetBillingProfileAsync(GetBillingProfileRequest request);
    Task<Result<object>> UpdateBillingProfileAsync(UpdateBillingProfileRequest request);
}

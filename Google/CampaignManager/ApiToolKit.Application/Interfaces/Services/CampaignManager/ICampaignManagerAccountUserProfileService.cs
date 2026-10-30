using ApiToolKit.Application.DTOs.CampaignManager.AccountUserProfile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAccountUserProfileService
{
    Task<Result<object>> InsertAccountUserProfileAsync(InsertAccountUserProfileRequest request);
    Task<Result<object>> ListAccountUserProfilesAsync(ListAccountUserProfilesRequest request);
    Task<Result<object>> UpdateAccountUserProfileAsync(UpdateAccountUserProfileRequest request);
    Task<Result<object>> PatchAccountUserProfileAsync(PatchAccountUserProfileRequest request);
    Task<Result<object>> GetAccountUserProfileAsync(GetAccountUserProfileRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.UserProfile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerUserProfileService
{
    Task<Result<object>> ListUserProfilesAsync(ListUserProfilesRequest request);
    Task<Result<object>> GetUserProfileAsync(GetUserProfileRequest request);
}

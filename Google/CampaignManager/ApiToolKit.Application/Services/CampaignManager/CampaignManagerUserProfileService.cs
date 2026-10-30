using ApiToolKit.Application.DTOs.CampaignManager.UserProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerUserProfileService : CampaignManagerServiceBase, ICampaignManagerUserProfileService
{
    public CampaignManagerUserProfileService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListUserProfilesAsync(ListUserProfilesRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/userprofiles";
        return await SendAsync(r.AccountKey, "ListUserProfiles", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetUserProfileAsync(GetUserProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}";
        return await SendAsync(r.AccountKey, "GetUserProfile", HttpMethod.Get, url);
    }
}

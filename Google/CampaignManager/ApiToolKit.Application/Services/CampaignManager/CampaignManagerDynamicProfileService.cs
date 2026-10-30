using ApiToolKit.Application.DTOs.CampaignManager.DynamicProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerDynamicProfileService : CampaignManagerServiceBase, ICampaignManagerDynamicProfileService
{
    public CampaignManagerDynamicProfileService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GenerateCodeDynamicProfileAsync(GenerateCodeDynamicProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicProfiles/{Esc(r.DynamicProfileId)}/generateCode";
        return await SendAsync(r.AccountKey, "GenerateCodeDynamicProfile", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateDynamicProfileAsync(UpdateDynamicProfileRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicProfiles";
        return await SendAsync(r.AccountKey, "UpdateDynamicProfile", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> InsertDynamicProfileAsync(InsertDynamicProfileRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicProfiles";
        return await SendAsync(r.AccountKey, "InsertDynamicProfile", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> PublishDynamicProfileAsync(PublishDynamicProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicProfiles/{Esc(r.DynamicProfileId)}/publish";
        return await SendAsync(r.AccountKey, "PublishDynamicProfile", HttpMethod.Post, url);
    }

    public async Task<Result<object>> GetDynamicProfileAsync(GetDynamicProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicProfiles/{Esc(r.DynamicProfileId)}";
        return await SendAsync(r.AccountKey, "GetDynamicProfile", HttpMethod.Get, url);
    }
}

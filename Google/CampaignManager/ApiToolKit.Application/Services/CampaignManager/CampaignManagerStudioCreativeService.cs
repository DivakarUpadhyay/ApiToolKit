using ApiToolKit.Application.DTOs.CampaignManager.StudioCreative;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerStudioCreativeService : CampaignManagerServiceBase, ICampaignManagerStudioCreativeService
{
    public CampaignManagerStudioCreativeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetStudioCreativeAsync(GetStudioCreativeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/creatives/{Esc(r.CreativeId)}";
        return await SendAsync(r.AccountKey, "GetStudioCreative", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertStudioCreativeAsync(InsertStudioCreativeRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/studio/creatives";
        return await SendAsync(r.AccountKey, "InsertStudioCreative", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> PublishStudioCreativeAsync(PublishStudioCreativeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/creatives/{Esc(r.CreativeId)}/publish";
        return await SendAsync(r.AccountKey, "PublishStudioCreative", HttpMethod.Post, url);
    }
}

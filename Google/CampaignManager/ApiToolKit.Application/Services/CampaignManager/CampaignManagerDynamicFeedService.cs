using ApiToolKit.Application.DTOs.CampaignManager.DynamicFeed;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerDynamicFeedService : CampaignManagerServiceBase, ICampaignManagerDynamicFeedService
{
    public CampaignManagerDynamicFeedService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetDynamicFeedAsync(GetDynamicFeedRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicFeeds/{Esc(r.DynamicFeedId)}";
        return await SendAsync(r.AccountKey, "GetDynamicFeed", HttpMethod.Get, url);
    }

    public async Task<Result<object>> RetransformDynamicFeedAsync(RetransformDynamicFeedRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicFeeds/{Esc(r.DynamicFeedId)}/retransform";
        return await SendAsync(r.AccountKey, "RetransformDynamicFeed", HttpMethod.Post, url);
    }

    public async Task<Result<object>> InsertDynamicFeedAsync(InsertDynamicFeedRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicFeeds";
        return await SendAsync(r.AccountKey, "InsertDynamicFeed", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateDynamicFeedAsync(UpdateDynamicFeedRequest r)
    {
        var url = "https://dfareporting.googleapis.com/dfareporting/v5/studio/dynamicFeeds";
        return await SendAsync(r.AccountKey, "UpdateDynamicFeed", HttpMethod.Put, url, null, r.Body);
    }
}

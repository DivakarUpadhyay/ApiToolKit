using ApiToolKit.Application.DTOs.DisplayVideo.Campaign;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCampaignService : DisplayVideoServiceBase, IDisplayVideoCampaignService
{
    public DisplayVideoCampaignService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateCampaignAsync(CreateCampaignRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/campaigns";
        return await SendAsync(r.AccountKey, "CreateCampaign", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListCampaignsAsync(ListCampaignsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/campaigns";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListCampaigns", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateCampaignAsync(UpdateCampaignRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/campaigns/{Esc(r.CampaignId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateCampaign", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetCampaignAsync(GetCampaignRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/campaigns/{Esc(r.CampaignId)}";
        return await SendAsync(r.AccountKey, "GetCampaign", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteCampaignAsync(DeleteCampaignRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/campaigns/{Esc(r.CampaignId)}";
        return await SendAsync(r.AccountKey, "DeleteCampaign", HttpMethod.Delete, url);
    }
}

using ApiToolKit.Application.DTOs.DisplayVideo.ChannelSite;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoChannelSiteService : DisplayVideoServiceBase, IDisplayVideoChannelSiteService
{
    public DisplayVideoChannelSiteService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateSiteAsync(CreateSiteRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}/sites";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateSite", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> ListSitesAsync(ListSitesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}/sites";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListSites", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ReplaceSiteAsync(ReplaceSiteRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}/sites:replace";
        return await SendAsync(r.AccountKey, "ReplaceSite", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> BulkEditSiteAsync(BulkEditSiteRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}/sites:bulkEdit";
        return await SendAsync(r.AccountKey, "BulkEditSite", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteSiteAsync(DeleteSiteRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}/sites/{Esc(r.SiteId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "DeleteSite", HttpMethod.Delete, url, query);
    }
}

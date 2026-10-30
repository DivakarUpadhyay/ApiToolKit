using ApiToolKit.Application.DTOs.DisplayVideo.Channel;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoChannelService : DisplayVideoServiceBase, IDisplayVideoChannelService
{
    public DisplayVideoChannelService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListChannelsAsync(ListChannelsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListChannels", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateChannelAsync(UpdateChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "UpdateChannel", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> CreateChannelAsync(CreateChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateChannel", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> GetChannelAsync(GetChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/channels/{Esc(r.ChannelId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetChannel", HttpMethod.Get, url, query);
    }
}

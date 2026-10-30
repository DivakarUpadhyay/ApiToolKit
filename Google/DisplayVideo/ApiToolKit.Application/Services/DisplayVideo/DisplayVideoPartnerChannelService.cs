using ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannel;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoPartnerChannelService : DisplayVideoServiceBase, IDisplayVideoPartnerChannelService
{
    public DisplayVideoPartnerChannelService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListChannelsAsync(ListChannelsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/channels";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListChannels", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetChannelAsync(GetChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/channels/{Esc(r.ChannelId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetChannel", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateChannelAsync(CreateChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/channels";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "CreateChannel", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateChannelAsync(UpdateChannelRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/channels/{Esc(r.ChannelId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateChannel", HttpMethod.Patch, url, query, r.Body);
    }
}

using ApiToolKit.Application.DTOs.DisplayVideo.Creative;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCreativeService : DisplayVideoServiceBase, IDisplayVideoCreativeService
{
    public DisplayVideoCreativeService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateCreativeAsync(UpdateCreativeRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/creatives/{Esc(r.CreativeId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateCreative", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> CreateCreativeAsync(CreateCreativeRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/creatives";
        return await SendAsync(r.AccountKey, "CreateCreative", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetCreativeAsync(GetCreativeRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/creatives/{Esc(r.CreativeId)}";
        return await SendAsync(r.AccountKey, "GetCreative", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListCreativesAsync(ListCreativesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/creatives";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListCreatives", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> DeleteCreativeAsync(DeleteCreativeRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/creatives/{Esc(r.CreativeId)}";
        return await SendAsync(r.AccountKey, "DeleteCreative", HttpMethod.Delete, url);
    }
}

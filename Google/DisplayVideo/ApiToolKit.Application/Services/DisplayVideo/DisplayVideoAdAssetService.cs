using ApiToolKit.Application.DTOs.DisplayVideo.AdAsset;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAdAssetService : DisplayVideoServiceBase, IDisplayVideoAdAssetService
{
    public DisplayVideoAdAssetService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAdAssetsAsync(ListAdAssetsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adAssets";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAdAssets", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> BulkCreateAdAssetAsync(BulkCreateAdAssetRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adAssets:bulkCreate";
        return await SendAsync(r.AccountKey, "BulkCreateAdAsset", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateAdAssetAsync(CreateAdAssetRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adAssets";
        return await SendAsync(r.AccountKey, "CreateAdAsset", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetAdAssetAsync(GetAdAssetRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adAssets/{Esc(r.AdAssetId)}";
        return await SendAsync(r.AccountKey, "GetAdAsset", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAdAssetAsync(UpdateAdAssetRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adAssets/{Esc(r.AdAssetId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAdAsset", HttpMethod.Patch, url, query, r.Body);
    }
}

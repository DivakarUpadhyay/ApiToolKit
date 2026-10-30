using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupAd;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAdGroupAdService : DisplayVideoServiceBase, IDisplayVideoAdGroupAdService
{
    public DisplayVideoAdGroupAdService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteAdGroupAdAsync(DeleteAdGroupAdRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroupAds/{Esc(r.AdGroupAdId)}";
        return await SendAsync(r.AccountKey, "DeleteAdGroupAd", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> UpdateAdGroupAdAsync(UpdateAdGroupAdRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroupAds/{Esc(r.AdGroupAdId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAdGroupAd", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetAdGroupAdAsync(GetAdGroupAdRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroupAds/{Esc(r.AdGroupAdId)}";
        return await SendAsync(r.AccountKey, "GetAdGroupAd", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAdGroupAdsAsync(ListAdGroupAdsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroupAds";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListAdGroupAds", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateAdGroupAdAsync(CreateAdGroupAdRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroupAds";
        return await SendAsync(r.AccountKey, "CreateAdGroupAd", HttpMethod.Post, url, null, r.Body);
    }
}

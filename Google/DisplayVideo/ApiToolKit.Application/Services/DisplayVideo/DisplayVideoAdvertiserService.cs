using ApiToolKit.Application.DTOs.DisplayVideo.Advertiser;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAdvertiserService : DisplayVideoServiceBase, IDisplayVideoAdvertiserService
{
    public DisplayVideoAdvertiserService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAdvertiserAsync(GetAdvertiserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}";
        return await SendAsync(r.AccountKey, "GetAdvertiser", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAssignedTargetingOptionsAdvertisersAsync(ListAssignedTargetingOptionsAdvertisersRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}:listAssignedTargetingOptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListAssignedTargetingOptionsAdvertisers", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> EditAssignedTargetingOptionsAdvertiserAsync(EditAssignedTargetingOptionsAdvertiserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}:editAssignedTargetingOptions";
        return await SendAsync(r.AccountKey, "EditAssignedTargetingOptionsAdvertiser", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateAdvertiserAsync(CreateAdvertiserRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/advertisers";
        return await SendAsync(r.AccountKey, "CreateAdvertiser", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateAdvertiserAsync(UpdateAdvertiserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAdvertiser", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> AuditAdvertiserAsync(AuditAdvertiserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}:audit";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask);
        return await SendAsync(r.AccountKey, "AuditAdvertiser", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListAdvertisersAsync(ListAdvertisersRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/advertisers";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAdvertisers", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> DeleteAdvertiserAsync(DeleteAdvertiserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}";
        return await SendAsync(r.AccountKey, "DeleteAdvertiser", HttpMethod.Delete, url);
    }
}

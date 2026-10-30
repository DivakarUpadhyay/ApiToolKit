using ApiToolKit.Application.DTOs.DisplayVideo.AdGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAdGroupService : DisplayVideoServiceBase, IDisplayVideoAdGroupService
{
    public DisplayVideoAdGroupService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> BulkEditAssignedTargetingOptionsAdGroupAsync(BulkEditAssignedTargetingOptionsAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups:bulkEditAssignedTargetingOptions";
        return await SendAsync(r.AccountKey, "BulkEditAssignedTargetingOptionsAdGroup", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> BulkListAssignedTargetingOptionsAdGroupAsync(BulkListAssignedTargetingOptionsAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups:bulkListAssignedTargetingOptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        AddMany(query, "adGroupIds", r.AdGroupIds);
        Add(query, "pageToken", r.PageToken);
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "BulkListAssignedTargetingOptionsAdGroup", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListAdGroupsAsync(ListAdGroupsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListAdGroups", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> DeleteAdGroupAsync(DeleteAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}";
        return await SendAsync(r.AccountKey, "DeleteAdGroup", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetAdGroupAsync(GetAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}";
        return await SendAsync(r.AccountKey, "GetAdGroup", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateAdGroupAsync(CreateAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups";
        return await SendAsync(r.AccountKey, "CreateAdGroup", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateAdGroupAsync(UpdateAdGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAdGroup", HttpMethod.Patch, url, query, r.Body);
    }
}

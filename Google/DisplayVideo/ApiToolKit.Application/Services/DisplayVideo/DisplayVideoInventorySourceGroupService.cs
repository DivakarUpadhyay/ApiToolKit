using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoInventorySourceGroupService : DisplayVideoServiceBase, IDisplayVideoInventorySourceGroupService
{
    public DisplayVideoInventorySourceGroupService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetInventorySourceGroupAsync(GetInventorySourceGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetInventorySourceGroup", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> DeleteInventorySourceGroupAsync(DeleteInventorySourceGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "DeleteInventorySourceGroup", HttpMethod.Delete, url, query);
    }

    public async Task<Result<object>> ListInventorySourceGroupsAsync(ListInventorySourceGroupsRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/inventorySourceGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListInventorySourceGroups", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateInventorySourceGroupAsync(UpdateInventorySourceGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "updateMask", r.UpdateMask);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "UpdateInventorySourceGroup", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> CreateInventorySourceGroupAsync(CreateInventorySourceGroupRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/inventorySourceGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "CreateInventorySourceGroup", HttpMethod.Post, url, query, r.Body);
    }
}

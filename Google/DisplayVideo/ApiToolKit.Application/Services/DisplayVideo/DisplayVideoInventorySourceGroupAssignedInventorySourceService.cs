using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroupAssignedInventorySource;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoInventorySourceGroupAssignedInventorySourceService : DisplayVideoServiceBase, IDisplayVideoInventorySourceGroupAssignedInventorySourceService
{
    public DisplayVideoInventorySourceGroupAssignedInventorySourceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAssignedInventorySourcesAsync(ListAssignedInventorySourcesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}/assignedInventorySources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAssignedInventorySources", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> BulkEditAssignedInventorySourceAsync(BulkEditAssignedInventorySourceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}/assignedInventorySources:bulkEdit";
        return await SendAsync(r.AccountKey, "BulkEditAssignedInventorySource", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateAssignedInventorySourceAsync(CreateAssignedInventorySourceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}/assignedInventorySources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "CreateAssignedInventorySource", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteAssignedInventorySourceAsync(DeleteAssignedInventorySourceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySourceGroups/{Esc(r.InventorySourceGroupId)}/assignedInventorySources/{Esc(r.AssignedInventorySourceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "DeleteAssignedInventorySource", HttpMethod.Delete, url, query);
    }
}

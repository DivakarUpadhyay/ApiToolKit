using ApiToolKit.Application.DTOs.DisplayVideo.InventorySource;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoInventorySourceService : DisplayVideoServiceBase, IDisplayVideoInventorySourceService
{
    public DisplayVideoInventorySourceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateInventorySourceAsync(UpdateInventorySourceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySources/{Esc(r.InventorySourceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "updateMask", r.UpdateMask);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "UpdateInventorySource", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetInventorySourceAsync(GetInventorySourceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySources/{Esc(r.InventorySourceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetInventorySource", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateInventorySourceAsync(CreateInventorySourceRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/inventorySources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateInventorySource", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> EditInventorySourceReadWriteAccessorsAsync(EditInventorySourceReadWriteAccessorsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/inventorySources/{Esc(r.InventorySourceId)}:editInventorySourceReadWriteAccessors";
        return await SendAsync(r.AccountKey, "EditInventorySourceReadWriteAccessors", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListInventorySourcesAsync(ListInventorySourcesRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/inventorySources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListInventorySources", HttpMethod.Get, url, query);
    }
}

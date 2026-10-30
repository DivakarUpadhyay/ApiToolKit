using ApiToolKit.Application.DTOs.DisplayVideo.LineItem;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoLineItemService : DisplayVideoServiceBase, IDisplayVideoLineItemService
{
    public DisplayVideoLineItemService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> BulkListAssignedTargetingOptionsLineItemAsync(BulkListAssignedTargetingOptionsLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems:bulkListAssignedTargetingOptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "lineItemIds", r.LineItemIds);
        return await SendAsync(r.AccountKey, "BulkListAssignedTargetingOptionsLineItem", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> BulkEditAssignedTargetingOptionsLineItemAsync(BulkEditAssignedTargetingOptionsLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems:bulkEditAssignedTargetingOptions";
        return await SendAsync(r.AccountKey, "BulkEditAssignedTargetingOptionsLineItem", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> BulkUpdateLineItemAsync(BulkUpdateLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems:bulkUpdate";
        return await SendAsync(r.AccountKey, "BulkUpdateLineItem", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteLineItemAsync(DeleteLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}";
        return await SendAsync(r.AccountKey, "DeleteLineItem", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetLineItemAsync(GetLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}";
        return await SendAsync(r.AccountKey, "GetLineItem", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateLineItemAsync(CreateLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems";
        return await SendAsync(r.AccountKey, "CreateLineItem", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateLineItemAsync(UpdateLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateLineItem", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> DuplicateLineItemAsync(DuplicateLineItemRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}:duplicate";
        return await SendAsync(r.AccountKey, "DuplicateLineItem", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListLineItemsAsync(ListLineItemsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListLineItems", HttpMethod.Get, url, query);
    }
}

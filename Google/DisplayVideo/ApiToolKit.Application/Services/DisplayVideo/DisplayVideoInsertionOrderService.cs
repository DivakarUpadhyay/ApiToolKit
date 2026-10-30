using ApiToolKit.Application.DTOs.DisplayVideo.InsertionOrder;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoInsertionOrderService : DisplayVideoServiceBase, IDisplayVideoInsertionOrderService
{
    public DisplayVideoInsertionOrderService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateInsertionOrderAsync(CreateInsertionOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/insertionOrders";
        return await SendAsync(r.AccountKey, "CreateInsertionOrder", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListInsertionOrdersAsync(ListInsertionOrdersRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/insertionOrders";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListInsertionOrders", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateInsertionOrderAsync(UpdateInsertionOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/insertionOrders/{Esc(r.InsertionOrderId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateInsertionOrder", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetInsertionOrderAsync(GetInsertionOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/insertionOrders/{Esc(r.InsertionOrderId)}";
        return await SendAsync(r.AccountKey, "GetInsertionOrder", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteInsertionOrderAsync(DeleteInsertionOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/insertionOrders/{Esc(r.InsertionOrderId)}";
        return await SendAsync(r.AccountKey, "DeleteInsertionOrder", HttpMethod.Delete, url);
    }
}

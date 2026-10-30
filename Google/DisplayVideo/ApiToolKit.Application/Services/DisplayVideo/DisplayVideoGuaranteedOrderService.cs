using ApiToolKit.Application.DTOs.DisplayVideo.GuaranteedOrder;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoGuaranteedOrderService : DisplayVideoServiceBase, IDisplayVideoGuaranteedOrderService
{
    public DisplayVideoGuaranteedOrderService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetGuaranteedOrderAsync(GetGuaranteedOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/guaranteedOrders/{Esc(r.GuaranteedOrderId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetGuaranteedOrder", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> EditGuaranteedOrderReadAccessorsAsync(EditGuaranteedOrderReadAccessorsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/guaranteedOrders/{Esc(r.GuaranteedOrderId)}:editGuaranteedOrderReadAccessors";
        return await SendAsync(r.AccountKey, "EditGuaranteedOrderReadAccessors", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateGuaranteedOrderAsync(UpdateGuaranteedOrderRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/guaranteedOrders/{Esc(r.GuaranteedOrderId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateGuaranteedOrder", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> ListGuaranteedOrdersAsync(ListGuaranteedOrdersRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/guaranteedOrders";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "filter", r.Filter);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListGuaranteedOrders", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateGuaranteedOrderAsync(CreateGuaranteedOrderRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/guaranteedOrders";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateGuaranteedOrder", HttpMethod.Post, url, query, r.Body);
    }
}

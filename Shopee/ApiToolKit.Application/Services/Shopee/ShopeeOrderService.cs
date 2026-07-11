using ApiToolKit.Application.DTOs.Shopee.Order;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeOrderService : IShopeeOrderService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeOrderRepository _orderRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeOrderService(IShopeeAuthRepository authRepo, IShopeeOrderRepository orderRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _orderRepo = orderRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<OrderListResponse>> GetOrderListAsync(GetOrderListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<OrderListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["time_range_field"] = "create_time",
            ["time_from"] = request.TimeFrom.ToString(),
            ["time_to"] = request.TimeTo.ToString(),
            ["page_size"] = request.PageSize.ToString(),
            ["order_status"] = request.OrderStatus
        };
        if (!string.IsNullOrEmpty(request.Cursor))
            queryParams["cursor"] = request.Cursor;

        var response = await _apiClient.GetAsync<OrderListResponse>("/api/v2/order/get_order_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<OrderListResponse>.Fail("No response from Shopee API.");
        if (!string.IsNullOrEmpty(response.Error)) return Result<OrderListResponse>.Fail(response.Message);
        return Result<OrderListResponse>.Ok(response);
    }

    public async Task<Result<List<OrderResponse>>> GetOrderDetailAsync(GetOrderDetailRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<List<OrderResponse>>.Fail("Access token not found.");

        var body = new
        {
            order_sn_list = request.OrderSnList,
            response_optional_fields = request.ResponseOptionalFields
        };
        var response = await _apiClient.PostAsync<dynamic>("/api/v2/order/get_order_detail", body, accessToken, request.ShopId);
        if (response == null) return Result<List<OrderResponse>>.Fail("No response from Shopee API.");

        var orders = JsonConvert.DeserializeObject<List<OrderResponse>>(JsonConvert.SerializeObject(response?.response?.order_list ?? new List<object>()));
        return Result<List<OrderResponse>>.Ok(orders ?? new List<OrderResponse>());
    }

    public async Task<Result<bool>> CancelOrderAsync(CancelOrderRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, cancel_reason = request.CancelReason, item_id = request.ItemId };
        var response = await _apiClient.PostAsync<dynamic>("/api/v2/order/cancel_order", body, accessToken, request.ShopId);
        if (response == null) return Result<bool>.Fail("No response from Shopee API.");
        await _orderRepo.UpdateOrderStatusAsync(request.ShopId, request.OrderSn, "CANCELLED");
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> SetOrderNoteAsync(SetNoteRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, note = request.Note };
        await _apiClient.PostAsync<dynamic>("/api/v2/order/set_note", body, accessToken, request.ShopId);
        await _orderRepo.SetOrderNoteAsync(request.ShopId, request.OrderSn, request.Note);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> HandleBuyerCancellationAsync(HandleBuyerCancellationRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, operation = request.Operation };
        await _apiClient.PostAsync<dynamic>("/api/v2/order/handle_buyer_cancellation", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetEscrowDetailAsync(long shopId, string orderSn)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["order_sn"] = orderSn };
        var response = await _apiClient.GetAsync<object>("/api/v2/payment/get_escrow_detail", queryParams, accessToken, shopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }
}



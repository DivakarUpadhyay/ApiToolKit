using ApiToolKit.Application.DTOs.Shopee.Logistics;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeLogisticsService : IShopeeLogisticsService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeLogisticsRepository _logisticsRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeLogisticsService(IShopeeAuthRepository authRepo, IShopeeLogisticsRepository logisticsRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _logisticsRepo = logisticsRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<LogisticsInfoResponse>> GetLogisticInfoAsync(GetLogisticInfoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<LogisticsInfoResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["order_sn"] = request.OrderSn };
        var response = await _apiClient.GetAsync<LogisticsInfoResponse>("/api/v2/logistics/get_shipping_parameter", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<LogisticsInfoResponse>.Fail("No response from Shopee API.");
        return Result<LogisticsInfoResponse>.Ok(response);
    }

    public async Task<Result<bool>> InitLogisticsAsync(InitLogisticsRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, package_number = request.PackageNumber, pickup = request.Pickup, dropoff = request.Dropoff, non_integrated = request.NonIntegrated };
        await _apiClient.PostAsync<dynamic>("/api/v2/logistics/init", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> ShipOrderAsync(ShipOrderRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, package_number = request.PackageNumber, pickup = request.Pickup, dropoff = request.Dropoff, non_integrated = request.NonIntegrated };
        await _apiClient.PostAsync<dynamic>("/api/v2/logistics/ship_order", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<TrackingNumberResponse>> GetTrackingNumberAsync(GetTrackingNumberRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<TrackingNumberResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["order_sn"] = request.OrderSn };
        if (!string.IsNullOrEmpty(request.PackageNumber)) queryParams["package_number"] = request.PackageNumber;
        var response = await _apiClient.GetAsync<TrackingNumberResponse>("/api/v2/logistics/get_tracking_number", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<TrackingNumberResponse>.Fail("No response from Shopee API.");
        return Result<TrackingNumberResponse>.Ok(response);
    }

    public async Task<Result<object>> GetOrderLogisticsAsync(long shopId, string orderSn)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["order_sn"] = orderSn };
        var response = await _apiClient.GetAsync<object>("/api/v2/logistics/get_tracking_info", queryParams, accessToken, shopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }

    public async Task<Result<bool>> UpdateLogisticsOrderAsync(UpdateLogisticsRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { order_sn = request.OrderSn, tracking_number = request.TrackingNumber, handover_method = request.HandoverMethod, branch_id = request.BranchId, sender_address_id = request.SenderAddressId, pickup_time_id = request.PickupTimeId };
        await _apiClient.PostAsync<dynamic>("/api/v2/logistics/update_shipping_order", body, accessToken, request.ShopId);
        await _logisticsRepo.UpdateTrackingNumberAsync(request.ShopId, request.OrderSn, request.TrackingNumber);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<AddressListResponse>> GetAddressListAsync(GetAddressListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<AddressListResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<AddressListResponse>("/api/v2/logistics/get_address_list", new Dictionary<string, string>(), accessToken, request.ShopId);
        if (response == null) return Result<AddressListResponse>.Fail("No response from Shopee API.");
        return Result<AddressListResponse>.Ok(response);
    }
}



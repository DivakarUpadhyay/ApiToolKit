using ApiToolKit.Application.DTOs.Shopee.FirstMile;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeFirstMileService : IShopeeFirstMileService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeFirstMileService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }

    public async Task<Result<GenerateFirstMileTrackingNumberResponse>> GenerateFirstMileTrackingNumberAsync(GenerateFirstMileTrackingNumberRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GenerateFirstMileTrackingNumberResponse>.Fail("Access token not found.");
        var body = new { quantity = request.quantity, logistics_channel_id = request.logistics_channel_id };
        var r = await _apiClient.PostAsync<GenerateFirstMileTrackingNumberResponse>("/api/v2/first_mile/generate_first_mile_tracking_number", body, token, request.ShopId);
        return r == null ? Result<GenerateFirstMileTrackingNumberResponse>.Fail("No response from Shopee API.") : Result<GenerateFirstMileTrackingNumberResponse>.Ok(r);
    }

    public async Task<Result<GetFirstMileTrackingNumberResponse>> GetFirstMileTrackingNumberAsync(GetFirstMileTrackingNumberRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetFirstMileTrackingNumberResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["first_mile_tracking_number"] = request.first_mile_tracking_number };
        var r = await _apiClient.GetAsync<GetFirstMileTrackingNumberResponse>("/api/v2/first_mile/get_tracking_number", q, token, request.ShopId);
        return r == null ? Result<GetFirstMileTrackingNumberResponse>.Fail("No response from Shopee API.") : Result<GetFirstMileTrackingNumberResponse>.Ok(r);
    }

    public async Task<Result<UploadFirstMileTrackingNumberResponse>> UploadFirstMileTrackingNumberAsync(UploadFirstMileTrackingNumberRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<UploadFirstMileTrackingNumberResponse>.Fail("Access token not found.");
        var body = new { first_mile_tracking_number = request.first_mile_tracking_number, order_sn_list = request.order_sn_list };
        var r = await _apiClient.PostAsync<UploadFirstMileTrackingNumberResponse>("/api/v2/first_mile/upload_first_mile_tracking_number", body, token, request.ShopId);
        return r == null ? Result<UploadFirstMileTrackingNumberResponse>.Fail("No response from Shopee API.") : Result<UploadFirstMileTrackingNumberResponse>.Ok(r);
    }

    public async Task<Result<UnbindFirstMileTrackingNumberResponse>> UnbindFirstMileTrackingNumberAsync(UnbindFirstMileTrackingNumberRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<UnbindFirstMileTrackingNumberResponse>.Fail("Access token not found.");
        var body = new { first_mile_tracking_number = request.first_mile_tracking_number };
        var r = await _apiClient.PostAsync<UnbindFirstMileTrackingNumberResponse>("/api/v2/first_mile/unbind_first_mile_tracking_number", body, token, request.ShopId);
        return r == null ? Result<UnbindFirstMileTrackingNumberResponse>.Fail("No response from Shopee API.") : Result<UnbindFirstMileTrackingNumberResponse>.Ok(r);
    }

    public async Task<Result<GetUnbindOrderListResponse>> GetUnbindOrderListAsync(GetUnbindOrderListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetUnbindOrderListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string>
        {
            ["page_no"] = request.page_no.ToString(),
            ["page_size"] = request.page_size.ToString(),
            ["pickup_time_from"] = request.pickup_time_from.ToString(),
            ["pickup_time_to"] = request.pickup_time_to.ToString()
        };
        var r = await _apiClient.GetAsync<GetUnbindOrderListResponse>("/api/v2/first_mile/get_unbind_order_list", q, token, request.ShopId);
        return r == null ? Result<GetUnbindOrderListResponse>.Fail("No response from Shopee API.") : Result<GetUnbindOrderListResponse>.Ok(r);
    }

    public async Task<Result<CreateChannelResponse>> CreateChannelAsync(CreateChannelRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<CreateChannelResponse>.Fail("Access token not found.");
        var body = new { order_sn_list = request.order_sn_list, logistics_channel_id = request.logistics_channel_id };
        var r = await _apiClient.PostAsync<CreateChannelResponse>("/api/v2/first_mile/create_channel", body, token, request.ShopId);
        return r == null ? Result<CreateChannelResponse>.Fail("No response from Shopee API.") : Result<CreateChannelResponse>.Ok(r);
    }

    public async Task<Result<GetFirstMileChannelListResponse>> GetFirstMileChannelListAsync(GetFirstMileChannelListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetFirstMileChannelListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["region"] = request.region };
        var r = await _apiClient.GetAsync<GetFirstMileChannelListResponse>("/api/v2/first_mile/get_channel_list", q, token, request.ShopId);
        return r == null ? Result<GetFirstMileChannelListResponse>.Fail("No response from Shopee API.") : Result<GetFirstMileChannelListResponse>.Ok(r);
    }

    public async Task<Result<GetFirstMileTrackingListResponse>> GetFirstMileTrackingListAsync(GetFirstMileTrackingListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetFirstMileTrackingListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string>
        {
            ["page_no"] = request.page_no.ToString(),
            ["page_size"] = request.page_size.ToString(),
            ["status"] = request.status
        };
        var r = await _apiClient.GetAsync<GetFirstMileTrackingListResponse>("/api/v2/first_mile/get_first_mile_tracking_list", q, token, request.ShopId);
        return r == null ? Result<GetFirstMileTrackingListResponse>.Fail("No response from Shopee API.") : Result<GetFirstMileTrackingListResponse>.Ok(r);
    }
}

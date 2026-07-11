using ApiToolKit.Application.DTOs.Shopee.Sbs;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeSbsService : IShopeeSbsService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeSbsService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }

    public async Task<Result<SbsGetItemListResponse>> GetItemListAsync(SbsGetItemListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SbsGetItemListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.page_no.ToString(), ["page_size"] = request.page_size.ToString() };
        if (!string.IsNullOrEmpty(request.item_status)) q["item_status"] = request.item_status;
        var r = await _apiClient.GetAsync<SbsGetItemListResponse>("/api/v2/sbs/get_item_list", q, token, request.ShopId);
        return r == null ? Result<SbsGetItemListResponse>.Fail("No response from Shopee API.") : Result<SbsGetItemListResponse>.Ok(r);
    }

    public async Task<Result<SbsCreateItemResponse>> CreateItemAsync(SbsCreateItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SbsCreateItemResponse>.Fail("Access token not found.");
        var body = new { item_id = request.item_id, price = request.price, stock = request.stock, logistics_channel_id = request.logistics_channel_id };
        var r = await _apiClient.PostAsync<SbsCreateItemResponse>("/api/v2/sbs/create_item", body, token, request.ShopId);
        return r == null ? Result<SbsCreateItemResponse>.Fail("No response from Shopee API.") : Result<SbsCreateItemResponse>.Ok(r);
    }

    public async Task<Result<SbsUpdateItemResponse>> UpdateItemAsync(SbsUpdateItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SbsUpdateItemResponse>.Fail("Access token not found.");
        var body = new { item_id = request.item_id, price = request.price, stock = request.stock };
        var r = await _apiClient.PostAsync<SbsUpdateItemResponse>("/api/v2/sbs/update_item", body, token, request.ShopId);
        return r == null ? Result<SbsUpdateItemResponse>.Fail("No response from Shopee API.") : Result<SbsUpdateItemResponse>.Ok(r);
    }

    public async Task<Result<SbsDeleteItemResponse>> DeleteItemAsync(SbsDeleteItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SbsDeleteItemResponse>.Fail("Access token not found.");
        var body = new { item_id_list = request.item_id_list };
        var r = await _apiClient.PostAsync<SbsDeleteItemResponse>("/api/v2/sbs/delete_item", body, token, request.ShopId);
        return r == null ? Result<SbsDeleteItemResponse>.Fail("No response from Shopee API.") : Result<SbsDeleteItemResponse>.Ok(r);
    }

    public async Task<Result<SbsGetItemResponse>> GetItemAsync(SbsGetItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SbsGetItemResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["item_id"] = request.item_id.ToString() };
        var r = await _apiClient.GetAsync<SbsGetItemResponse>("/api/v2/sbs/get_item", q, token, request.ShopId);
        return r == null ? Result<SbsGetItemResponse>.Fail("No response from Shopee API.") : Result<SbsGetItemResponse>.Ok(r);
    }
}

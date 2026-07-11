using ApiToolKit.Application.DTOs.Shopee.Fbs;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeFbsService : IShopeeFbsService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeFbsService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }

    public async Task<Result<FbsGetItemListResponse>> GetItemListAsync(FbsGetItemListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<FbsGetItemListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.page_no.ToString(), ["page_size"] = request.page_size.ToString() };
        var r = await _apiClient.GetAsync<FbsGetItemListResponse>("/api/v2/fbs/get_item_list", q, token, request.ShopId);
        return r == null ? Result<FbsGetItemListResponse>.Fail("No response from Shopee API.") : Result<FbsGetItemListResponse>.Ok(r);
    }

    public async Task<Result<FbsCreateItemResponse>> CreateItemAsync(FbsCreateItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<FbsCreateItemResponse>.Fail("Access token not found.");
        var body = new { item_id = request.item_id, sku_list = request.sku_list };
        var r = await _apiClient.PostAsync<FbsCreateItemResponse>("/api/v2/fbs/create_item", body, token, request.ShopId);
        return r == null ? Result<FbsCreateItemResponse>.Fail("No response from Shopee API.") : Result<FbsCreateItemResponse>.Ok(r);
    }

    public async Task<Result<FbsUpdateItemResponse>> UpdateItemAsync(FbsUpdateItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<FbsUpdateItemResponse>.Fail("Access token not found.");
        var body = new { item_id = request.item_id, sku_list = request.sku_list };
        var r = await _apiClient.PostAsync<FbsUpdateItemResponse>("/api/v2/fbs/update_item", body, token, request.ShopId);
        return r == null ? Result<FbsUpdateItemResponse>.Fail("No response from Shopee API.") : Result<FbsUpdateItemResponse>.Ok(r);
    }

    public async Task<Result<FbsDeleteItemResponse>> DeleteItemAsync(FbsDeleteItemRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<FbsDeleteItemResponse>.Fail("Access token not found.");
        var body = new { item_id_list = request.item_id_list };
        var r = await _apiClient.PostAsync<FbsDeleteItemResponse>("/api/v2/fbs/delete_item", body, token, request.ShopId);
        return r == null ? Result<FbsDeleteItemResponse>.Fail("No response from Shopee API.") : Result<FbsDeleteItemResponse>.Ok(r);
    }
}

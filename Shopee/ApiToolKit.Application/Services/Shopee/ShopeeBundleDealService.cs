using ApiToolKit.Application.DTOs.Shopee.BundleDeal;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
namespace ApiToolKit.Application.Services.Shopee;
public class ShopeeBundleDealService : IShopeeBundleDealService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;
    public ShopeeBundleDealService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient) { _authRepo = authRepo; _apiClient = apiClient; }
    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }
    public async Task<Result<BundleDealResponse>> AddBundleDealAsync(AddBundleDealRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<BundleDealResponse>.Fail("Access token not found.");
        var body = new { bundle_deal_name = r.BundleDealName, start_time = r.StartTime, end_time = r.EndTime, item_list = r.ItemList.Select(i => new { item_id = i.ItemId, bundle_price = i.BundlePrice, model_id = i.ModelId }) };
        var res = await _apiClient.PostAsync<BundleDealResponse>("/api/v2/bundle_deal/add_bundle_deal", body, token, r.ShopId);
        return res == null ? Result<BundleDealResponse>.Fail("No response.") : Result<BundleDealResponse>.Ok(res);
    }
    public async Task<Result<BundleDealResponse>> UpdateBundleDealAsync(UpdateBundleDealRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<BundleDealResponse>.Fail("Access token not found.");
        var res = await _apiClient.PostAsync<BundleDealResponse>("/api/v2/bundle_deal/update_bundle_deal", new { bundle_deal_id = r.BundleDealId, bundle_deal_name = r.BundleDealName, start_time = r.StartTime, end_time = r.EndTime }, token, r.ShopId);
        return res == null ? Result<BundleDealResponse>.Fail("No response.") : Result<BundleDealResponse>.Ok(res);
    }
    public async Task<Result<bool>> DeleteBundleDealAsync(DeleteBundleDealRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/bundle_deal/delete_bundle_deal", new { bundle_deal_id = r.BundleDealId }, token, r.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> EndBundleDealAsync(EndBundleDealRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/bundle_deal/end_bundle_deal", new { bundle_deal_id = r.BundleDealId }, token, r.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<BundleDealResponse>> GetBundleDealAsync(GetBundleDealRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<BundleDealResponse>.Fail("Access token not found.");
        var res = await _apiClient.GetAsync<BundleDealResponse>("/api/v2/bundle_deal/get_bundle_deal", new Dictionary<string, string> { ["bundle_deal_id"] = r.BundleDealId.ToString() }, token, r.ShopId);
        return res == null ? Result<BundleDealResponse>.Fail("No response.") : Result<BundleDealResponse>.Ok(res);
    }
    public async Task<Result<BundleDealListResponse>> GetBundleDealListAsync(GetBundleDealListRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<BundleDealListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["status"] = r.Status, ["page_no"] = r.PageNo.ToString(), ["page_size"] = r.PageSize.ToString() };
        var res = await _apiClient.GetAsync<BundleDealListResponse>("/api/v2/bundle_deal/get_bundle_deal_list", q, token, r.ShopId);
        return res == null ? Result<BundleDealListResponse>.Fail("No response.") : Result<BundleDealListResponse>.Ok(res);
    }
    public async Task<Result<object>> GetBundleDealItemAsync(long shopId, long bundleDealId) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var res = await _apiClient.GetAsync<object>("/api/v2/bundle_deal/get_bundle_deal_item", new Dictionary<string, string> { ["bundle_deal_id"] = bundleDealId.ToString() }, token, shopId);
        return res == null ? Result<object>.Fail("No response.") : Result<object>.Ok(res);
    }
    public async Task<Result<bool>> AddBundleDealItemAsync(AddBundleDealItemRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/bundle_deal/add_bundle_deal_item", new { bundle_deal_id = r.BundleDealId, item_list = r.ItemList }, token, r.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> DeleteBundleDealItemAsync(DeleteBundleDealItemRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/bundle_deal/delete_bundle_deal_item", new { bundle_deal_id = r.BundleDealId, item_id_list = r.ItemIdList }, token, r.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> UpdateBundleDealItemAsync(UpdateBundleDealItemRequest r) {
        var token = await GetToken(r.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/bundle_deal/update_bundle_deal_item", new { bundle_deal_id = r.BundleDealId, item_list = r.ItemList }, token, r.ShopId);
        return Result<bool>.Ok(true);
    }
}

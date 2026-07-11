using ApiToolKit.Application.DTOs.Shopee.Discount;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeDiscountService : IShopeeDiscountService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeDiscountRepository _discountRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeDiscountService(IShopeeAuthRepository authRepo, IShopeeDiscountRepository discountRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _discountRepo = discountRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<DiscountResponse>> AddDiscountAsync(AddDiscountRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DiscountResponse>.Fail("Access token not found.");

        var body = new { discount_name = request.DiscountName, start_time = request.StartTime, end_time = request.EndTime, item_list = request.ItemList.Select(i => new { item_id = i.ItemId, item_promotion_price = i.ItemPromotionPrice, model_id = i.ModelId, purchase_limit = i.PurchaseLimit }) };
        var response = await _apiClient.PostAsync<DiscountResponse>("/api/v2/discount/add_discount", body, accessToken, request.ShopId);
        if (response == null) return Result<DiscountResponse>.Fail("No response from Shopee API.");
        return Result<DiscountResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteDiscountAsync(DeleteDiscountRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId };
        await _apiClient.PostAsync<dynamic>("/api/v2/discount/delete_discount", body, accessToken, request.ShopId);
        await _discountRepo.DeleteDiscountAsync(request.ShopId, request.DiscountId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<DiscountResponse>> UpdateDiscountAsync(UpdateDiscountRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DiscountResponse>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId, discount_name = request.DiscountName, start_time = request.StartTime, end_time = request.EndTime };
        var response = await _apiClient.PostAsync<DiscountResponse>("/api/v2/discount/update_discount", body, accessToken, request.ShopId);
        if (response == null) return Result<DiscountResponse>.Fail("No response from Shopee API.");
        return Result<DiscountResponse>.Ok(response);
    }

    public async Task<Result<bool>> EndDiscountAsync(EndDiscountRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId };
        await _apiClient.PostAsync<dynamic>("/api/v2/discount/end_discount", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<DiscountResponse>> GetDiscountAsync(GetDiscountRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DiscountResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["discount_id"] = request.DiscountId.ToString() };
        var response = await _apiClient.GetAsync<DiscountResponse>("/api/v2/discount/get_discount", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<DiscountResponse>.Fail("No response from Shopee API.");
        return Result<DiscountResponse>.Ok(response);
    }

    public async Task<Result<DiscountListResponse>> GetDiscountListAsync(GetDiscountListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DiscountListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["discount_status"] = request.DiscountStatus, ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<DiscountListResponse>("/api/v2/discount/get_discount_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<DiscountListResponse>.Fail("No response from Shopee API.");
        return Result<DiscountListResponse>.Ok(response);
    }

    public async Task<Result<bool>> AddDiscountItemAsync(AddDiscountItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId, item_list = request.ItemList };
        await _apiClient.PostAsync<dynamic>("/api/v2/discount/add_discount_item", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteDiscountItemAsync(DeleteDiscountItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId, item_id_list = request.ItemIdList };
        await _apiClient.PostAsync<dynamic>("/api/v2/discount/delete_discount_item", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> UpdateDiscountItemAsync(UpdateDiscountItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { discount_id = request.DiscountId, item_list = request.ItemList };
        await _apiClient.PostAsync<dynamic>("/api/v2/discount/update_discount_item", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }
}



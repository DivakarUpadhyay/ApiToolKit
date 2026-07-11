using ApiToolKit.Application.DTOs.Shopee.Shop;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeShopService : IShopeeShopService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeShopRepository _shopRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeShopService(IShopeeAuthRepository authRepo, IShopeeShopRepository shopRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _shopRepo = shopRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<ShopInfoResponse>> GetShopInfoAsync(long shopId)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<ShopInfoResponse>.Fail("Access token not found. Please authorize first.");

        var response = await _apiClient.GetAsync<ShopInfoResponse>("/api/v2/shop/get_shop_info", new Dictionary<string, string>(), accessToken, shopId);
        if (response == null) return Result<ShopInfoResponse>.Fail("No response from Shopee API.");
        if (!string.IsNullOrEmpty(response.Error)) return Result<ShopInfoResponse>.Fail(response.Message);

        await _shopRepo.SaveShopInfoAsync(shopId, response.ShopName, response.Region, response.ShopStatus, response.ShopDescription, response.Avatar);
        return Result<ShopInfoResponse>.Ok(response);
    }

    public async Task<Result<ShopListResponse>> GetShopsByPartnerAsync(int pageNo, int pageSize)
    {
        var queryParams = new Dictionary<string, string> { ["page_no"] = pageNo.ToString(), ["page_size"] = pageSize.ToString() };
        var response = await _apiClient.GetAsync<ShopListResponse>("/api/v2/shop/get_shops_by_partner", queryParams);
        if (response == null) return Result<ShopListResponse>.Fail("No response from Shopee API.");
        return Result<ShopListResponse>.Ok(response);
    }

    public async Task<Result<ShopInfoResponse>> GetShopProfileAsync(long shopId)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<ShopInfoResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ShopInfoResponse>("/api/v2/shop/get_profile", new Dictionary<string, string>(), accessToken, shopId);
        if (response == null) return Result<ShopInfoResponse>.Fail("No response from Shopee API.");
        if (!string.IsNullOrEmpty(response.Error)) return Result<ShopInfoResponse>.Fail(response.Message);
        return Result<ShopInfoResponse>.Ok(response);
    }

    public async Task<Result<bool>> UpdateShopProfileAsync(UpdateShopProfileRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { shop_name = request.ShopName, description = request.ShopDescription, avatar = request.Avatar };
        var response = await _apiClient.PostAsync<dynamic>("/api/v2/shop/update_profile", body, accessToken, request.ShopId);
        if (response == null) return Result<bool>.Fail("No response from Shopee API.");
        return Result<bool>.Ok(true);
    }
}



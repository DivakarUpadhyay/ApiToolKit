using ApiToolKit.Application.DTOs.Shopee.Merchant;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeMerchantService : IShopeeMerchantService
{
    private readonly IShopeeApiClient _apiClient;

    public ShopeeMerchantService(IShopeeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<MerchantInfoResponse>> GetMerchantInfoAsync(GetMerchantInfoRequest request)
    {
        var queryParams = new Dictionary<string, string> { ["merchant_id"] = request.MerchantId.ToString() };
        var response = await _apiClient.GetAsync<MerchantInfoResponse>("/api/v2/merchant/get_merchant_info", queryParams);
        if (response == null) return Result<MerchantInfoResponse>.Fail("No response from Shopee API.");
        return Result<MerchantInfoResponse>.Ok(response);
    }

    public async Task<Result<ShopsByMerchantResponse>> GetShopsByMerchantAsync(GetShopsByMerchantRequest request)
    {
        var queryParams = new Dictionary<string, string> { ["merchant_id"] = request.MerchantId.ToString(), ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<ShopsByMerchantResponse>("/api/v2/merchant/get_shop_list_by_merchant", queryParams);
        if (response == null) return Result<ShopsByMerchantResponse>.Fail("No response from Shopee API.");
        return Result<ShopsByMerchantResponse>.Ok(response);
    }
}



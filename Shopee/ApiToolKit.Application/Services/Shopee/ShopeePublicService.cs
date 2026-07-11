using ApiToolKit.Application.DTOs.Shopee.Public;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeePublicService : IShopeePublicService
{
    private readonly IShopeeApiClient _client;

    public ShopeePublicService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<GetShopsByPartnerResponse>> GetShopsByPartnerAsync(GetShopsByPartnerRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetShopsByPartnerResponse>("/api/v2/public/get_shops_by_partner", queryParams);
            if (response == null) return Result<GetShopsByPartnerResponse>.Fail("No response from Shopee API.");
            return Result<GetShopsByPartnerResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetShopsByPartnerResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCategoriesByCountryResponse>> GetCategoriesByCountryAsync(GetCategoriesByCountryRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["country"] = request.Country,
                ["language"] = request.Language
            };
            var response = await _client.GetAsync<GetCategoriesByCountryResponse>("/api/v2/public/get_categories_by_country", queryParams);
            if (response == null) return Result<GetCategoriesByCountryResponse>.Fail("No response from Shopee API.");
            return Result<GetCategoriesByCountryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetCategoriesByCountryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetPaymentMethodListResponse>> GetPaymentMethodListAsync(GetPaymentMethodListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["region"] = request.Region };
            var response = await _client.GetAsync<GetPaymentMethodListResponse>("/api/v2/public/get_payment_method_list", queryParams);
            if (response == null) return Result<GetPaymentMethodListResponse>.Fail("No response from Shopee API.");
            return Result<GetPaymentMethodListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetPaymentMethodListResponse>.Fail(ex.Message);
        }
    }
}

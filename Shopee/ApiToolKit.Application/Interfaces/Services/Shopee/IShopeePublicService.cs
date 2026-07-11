using ApiToolKit.Application.DTOs.Shopee.Public;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeePublicService
{
    Task<Result<GetShopsByPartnerResponse>> GetShopsByPartnerAsync(GetShopsByPartnerRequest request);
    Task<Result<GetCategoriesByCountryResponse>> GetCategoriesByCountryAsync(GetCategoriesByCountryRequest request);
    Task<Result<GetPaymentMethodListResponse>> GetPaymentMethodListAsync(GetPaymentMethodListRequest request);
}

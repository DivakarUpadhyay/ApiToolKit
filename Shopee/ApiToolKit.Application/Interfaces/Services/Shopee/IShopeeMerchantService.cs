using ApiToolKit.Application.DTOs.Shopee.Merchant;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeMerchantService
{
    Task<Result<MerchantInfoResponse>> GetMerchantInfoAsync(GetMerchantInfoRequest request);
    Task<Result<ShopsByMerchantResponse>> GetShopsByMerchantAsync(GetShopsByMerchantRequest request);
}

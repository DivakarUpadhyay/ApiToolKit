using ApiToolKit.Application.DTOs.Shopee.Shop;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeShopService
{
    Task<Result<ShopInfoResponse>> GetShopInfoAsync(long shopId);
    Task<Result<ShopListResponse>> GetShopsByPartnerAsync(int pageNo, int pageSize);
    Task<Result<ShopInfoResponse>> GetShopProfileAsync(long shopId);
    Task<Result<bool>> UpdateShopProfileAsync(UpdateShopProfileRequest request);
}

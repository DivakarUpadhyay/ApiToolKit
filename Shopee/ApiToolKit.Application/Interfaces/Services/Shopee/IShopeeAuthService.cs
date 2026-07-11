using ApiToolKit.Application.DTOs.Shopee.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeAuthService
{
    Task<Result<ShopeeAuthUrlResponse>> GetAuthUrlAsync();
    Task<Result<AccessTokenResponse>> GetAccessTokenAsync(GetAccessTokenRequest request);
    Task<Result<AccessTokenResponse>> RefreshAccessTokenAsync(RefreshAccessTokenRequest request);
}

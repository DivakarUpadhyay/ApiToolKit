using ApiToolKit.Application.DTOs.Shopee.Auth;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopee;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeAuthService : IShopeeAuthService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeAuthService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ShopeeAuthUrlResponse>> GetAuthUrlAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ShopeeAuthUrlResponse>.Fail("Shopee configuration not found.");

        var url = _apiClient.BuildAuthUrl(config.RedirectUrl);
        return Result<ShopeeAuthUrlResponse>.Ok(new ShopeeAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<AccessTokenResponse>> GetAccessTokenAsync(GetAccessTokenRequest request)
    {
        var body = new { code = request.Code, shop_id = request.ShopId, partner_id = 0L };
        var response = await _apiClient.PostAsync<AccessTokenResponse>("/api/v2/auth/token/get", body);
        if (response == null)
            return Result<AccessTokenResponse>.Fail("No response from Shopee API.");

        if (!string.IsNullOrEmpty(response.Error))
            return Result<AccessTokenResponse>.Fail(response.Message);

        await _authRepo.SaveTokenAsync(new ShopeeToken
        {
            ShopId = request.ShopId,
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            ExpireIn = response.ExpireIn,
            RefreshTokenExpireIn = response.RefreshTokenExpireIn
        });

        await _authRepo.SaveApiLogAsync(new ShopeeApiLog
        {
            ShopId = request.ShopId,
            ApiName = "GetAccessToken",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200
        });

        return Result<AccessTokenResponse>.Ok(response);
    }

    public async Task<Result<AccessTokenResponse>> RefreshAccessTokenAsync(RefreshAccessTokenRequest request)
    {
        var body = new { refresh_token = request.RefreshToken, shop_id = request.ShopId, partner_id = 0L };
        var response = await _apiClient.PostAsync<AccessTokenResponse>("/api/v2/auth/access_token/get", body);
        if (response == null)
            return Result<AccessTokenResponse>.Fail("No response from Shopee API.");

        if (!string.IsNullOrEmpty(response.Error))
            return Result<AccessTokenResponse>.Fail(response.Message);

        await _authRepo.UpdateTokenAsync(new ShopeeToken
        {
            ShopId = request.ShopId,
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            ExpireIn = response.ExpireIn,
            RefreshTokenExpireIn = response.RefreshTokenExpireIn
        });

        return Result<AccessTokenResponse>.Ok(response);
    }
}



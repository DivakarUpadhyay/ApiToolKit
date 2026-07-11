using ApiToolKit.Application.DTOs.Shopify.Auth;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyAuthService : IShopifyAuthService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyAuthService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GetInstallUrlResponse>> GetInstallUrlAsync(GetInstallUrlRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<GetInstallUrlResponse>.Fail("Shopify configuration not found.");

        var state = request.State ?? Guid.NewGuid().ToString("N");
        var installUrl = _apiClient.BuildInstallUrl(
            request.ShopDomain, config.ApiKey, config.Scopes, config.RedirectUrl, state);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = request.ShopDomain,
            ApiName = "GetInstallUrl",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GetInstallUrlResponse>.Ok(new GetInstallUrlResponse
        {
            InstallUrl = installUrl,
            State = state
        });
    }

    public async Task<Result<CallbackResponse>> HandleCallbackAsync(CallbackRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CallbackResponse>.Fail("Shopify configuration not found.");

        // Verify HMAC
        var isValid = VerifyHmac(request, config.ApiSecret);
        if (!isValid)
            return Result<CallbackResponse>.Fail("HMAC verification failed. Request is not from Shopify.");

        var tokenResponse = await _apiClient.ExchangeCodeAsync<dynamic>(
            request.Shop, config.ApiKey, config.ApiSecret, request.Code);

        if (tokenResponse == null)
            return Result<CallbackResponse>.Fail("Failed to exchange code for access token.");

        string accessToken = tokenResponse?.access_token?.ToString() ?? string.Empty;
        string scope = tokenResponse?.scope?.ToString() ?? string.Empty;

        var existingToken = await _authRepo.GetTokenByShopAsync(request.Shop);
        if (existingToken != null)
        {
            existingToken.AccessToken = accessToken;
            existingToken.Scope = scope;
            existingToken.UpdatedAt = DateTime.UtcNow;
            await _authRepo.UpdateTokenAsync(existingToken);
        }
        else
        {
            await _authRepo.SaveTokenAsync(new ShopifyToken
            {
                ShopDomain = request.Shop,
                AccessToken = accessToken,
                Scope = scope,
                TokenType = "offline",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = request.Shop,
            ApiName = "HandleCallback",
            RequestBody = JsonConvert.SerializeObject(new { code = request.Code, shop = request.Shop }),
            ResponseBody = JsonConvert.SerializeObject(tokenResponse),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CallbackResponse>.Ok(new CallbackResponse
        {
            ShopDomain = request.Shop,
            AccessToken = accessToken,
            Scope = scope
        });
    }

    public async Task<Result<ShopInfoResponse>> GetShopInfoAsync(string shopDomain)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ShopInfoResponse>.Fail("No access token found for this shop.");

        var response = await _apiClient.GetAsync<dynamic>(shopDomain, "shop.json", null, token.AccessToken);
        if (response == null)
            return Result<ShopInfoResponse>.Fail("No response from Shopify API.");

        var shop = response?.shop;

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetShopInfo",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<ShopInfoResponse>.Ok(new ShopInfoResponse
        {
            Id = shop?.id?.ToString() ?? string.Empty,
            Name = shop?.name?.ToString() ?? string.Empty,
            Email = shop?.email?.ToString() ?? string.Empty,
            Domain = shop?.domain?.ToString() ?? string.Empty,
            MyshopifyDomain = shop?.myshopify_domain?.ToString() ?? string.Empty,
            PlanName = shop?.plan_name?.ToString() ?? string.Empty,
            Currency = shop?.currency?.ToString() ?? string.Empty,
            CountryCode = shop?.country_code?.ToString() ?? string.Empty,
            TimezoneAbbreviation = shop?.timezone?.ToString() ?? string.Empty,
            CreatedAt = shop?.created_at?.ToString() ?? string.Empty
        });
    }

    public async Task<Result<bool>> VerifyHmacAsync(VerifyHmacRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var queryParams = System.Web.HttpUtility.ParseQueryString(request.QueryString);
        var hmac = queryParams["hmac"] ?? string.Empty;
        queryParams.Remove("hmac");

        var sortedParams = queryParams.AllKeys
            .Where(k => k != null)
            .OrderBy(k => k)
            .Select(k => $"{k}={queryParams[k]}")
            .ToList();

        var message = string.Join("&", sortedParams);
        using var hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(config.ApiSecret));
        var hash = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        var computedHmac = Convert.ToHexString(hash).ToLower();

        var isValid = computedHmac == hmac.ToLower();
        return Result<bool>.Ok(isValid);
    }

    public async Task<Result<ShopifyTokenResponse>> GetStoredTokenAsync(string shopDomain)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ShopifyTokenResponse>.Fail("No token found for this shop.");

        return Result<ShopifyTokenResponse>.Ok(new ShopifyTokenResponse
        {
            ShopDomain = token.ShopDomain,
            AccessToken = token.AccessToken,
            Scope = token.Scope,
            TokenType = token.TokenType,
            CreatedAt = token.CreatedAt
        });
    }

    public async Task<Result<bool>> RevokeTokenAsync(string shopDomain)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail("No token found for this shop.");

        token.AccessToken = string.Empty;
        token.UpdatedAt = DateTime.UtcNow;
        await _authRepo.UpdateTokenAsync(token);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "RevokeToken",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true, "Token revoked successfully.");
    }

    private static bool VerifyHmac(CallbackRequest request, string apiSecret)
    {
        var message = $"code={request.Code}&shop={request.Shop}&state={request.State}&timestamp={request.Timestamp}";
        using var hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
        var hash = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        var computedHmac = Convert.ToHexString(hash).ToLower();
        return computedHmac == request.Hmac.ToLower();
    }
}

using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyThemeService : IShopifyThemeService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyThemeService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListThemesResponse>> ListThemesAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListThemesResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListThemesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListThemesResponse>(
            shopDomain,
            "themes.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListThemes",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListThemesResponse>.Fail("No response from Shopify API.");

        return Result<ListThemesResponse>.Ok(response);
    }

    public async Task<Result<ThemeResponse>> GetThemeAsync(string shopDomain, string themeId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ThemeResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ThemeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ThemeResponse>(
            shopDomain,
            $"themes/{themeId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetTheme",
            RequestBody = JsonConvert.SerializeObject(new { themeId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Theme != null,
            StatusCode = response?.Theme != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ThemeResponse>.Fail("No response from Shopify API.");

        return Result<ThemeResponse>.Ok(response);
    }

    public async Task<Result<ThemeResponse>> CreateThemeAsync(string shopDomain, CreateThemeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ThemeResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ThemeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { theme = request };

        var response = await _apiClient.PostAsync<ThemeResponse>(
            shopDomain,
            "themes.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateTheme",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Theme != null,
            StatusCode = response?.Theme != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ThemeResponse>.Fail("No response from Shopify API.");

        return Result<ThemeResponse>.Ok(response);
    }

    public async Task<Result<ThemeResponse>> UpdateThemeAsync(string shopDomain, string themeId, UpdateThemeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ThemeResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ThemeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { theme = request };

        var response = await _apiClient.PutAsync<ThemeResponse>(
            shopDomain,
            $"themes/{themeId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateTheme",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Theme != null,
            StatusCode = response?.Theme != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ThemeResponse>.Fail("No response from Shopify API.");

        return Result<ThemeResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteThemeAsync(string shopDomain, string themeId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"themes/{themeId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteTheme",
            RequestBody = JsonConvert.SerializeObject(new { themeId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}

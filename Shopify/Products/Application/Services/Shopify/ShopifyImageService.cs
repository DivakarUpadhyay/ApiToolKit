using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyImageService : IShopifyImageService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyImageService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListImagesResponse>> ListImagesAsync(string shopDomain, string productId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListImagesResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListImagesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListImagesResponse>(
            shopDomain,
            $"products/{productId}/images.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListImages",
            RequestBody = JsonConvert.SerializeObject(new { productId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListImagesResponse>.Fail("No response from Shopify API.");

        return Result<ListImagesResponse>.Ok(response);
    }

    public async Task<Result<ImageResponse>> GetImageAsync(string shopDomain, string productId, string imageId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ImageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ImageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ImageResponse>(
            shopDomain,
            $"products/{productId}/images/{imageId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetImage",
            RequestBody = JsonConvert.SerializeObject(new { productId, imageId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Image != null,
            StatusCode = response?.Image != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ImageResponse>.Fail("No response from Shopify API.");

        return Result<ImageResponse>.Ok(response);
    }

    public async Task<Result<int>> CountImagesAsync(string shopDomain, string productId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"products/{productId}/images/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountImages",
            RequestBody = JsonConvert.SerializeObject(new { productId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<ImageResponse>> CreateImageAsync(string shopDomain, string productId, CreateImageRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ImageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ImageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { image = request };

        var response = await _apiClient.PostAsync<ImageResponse>(
            shopDomain,
            $"products/{productId}/images.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateImage",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Image != null,
            StatusCode = response?.Image != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ImageResponse>.Fail("No response from Shopify API.");

        return Result<ImageResponse>.Ok(response);
    }

    public async Task<Result<ImageResponse>> UpdateImageAsync(string shopDomain, string productId, string imageId, UpdateImageRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ImageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ImageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { image = request };

        var response = await _apiClient.PutAsync<ImageResponse>(
            shopDomain,
            $"products/{productId}/images/{imageId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateImage",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Image != null,
            StatusCode = response?.Image != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ImageResponse>.Fail("No response from Shopify API.");

        return Result<ImageResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteImageAsync(string shopDomain, string productId, string imageId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"products/{productId}/images/{imageId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteImage",
            RequestBody = JsonConvert.SerializeObject(new { productId, imageId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}

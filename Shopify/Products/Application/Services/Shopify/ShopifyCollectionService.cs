using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyCollectionService : IShopifyCollectionService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyCollectionService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    // ---- Custom Collections ----

    public async Task<Result<ListCollectionsResponse>> ListCustomCollectionsAsync(string shopDomain, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListCollectionsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListCollectionsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var raw = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "custom_collections.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListCustomCollections",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<ListCollectionsResponse>.Fail("No response from Shopify API.");

        var list = JsonConvert.DeserializeObject<List<CollectionDto>>(JsonConvert.SerializeObject(raw?.custom_collections)) ?? new List<CollectionDto>();
        return Result<ListCollectionsResponse>.Ok(new ListCollectionsResponse { CustomCollections = list });
    }

    public async Task<Result<CollectionResponse>> GetCustomCollectionAsync(string shopDomain, string collectionId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var raw = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"custom_collections/{collectionId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetCustomCollection",
            RequestBody = JsonConvert.SerializeObject(new { collectionId }),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.custom_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { CustomCollection = dto });
    }

    public async Task<Result<int>> CountCustomCollectionsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "custom_collections/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountCustomCollections",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<CollectionResponse>> CreateCustomCollectionAsync(string shopDomain, CreateCollectionRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { custom_collection = request };

        var raw = await _apiClient.PostAsync<dynamic>(
            shopDomain,
            "custom_collections.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateCustomCollection",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.custom_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { CustomCollection = dto });
    }

    public async Task<Result<CollectionResponse>> UpdateCustomCollectionAsync(string shopDomain, string collectionId, UpdateCollectionRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { custom_collection = request };

        var raw = await _apiClient.PutAsync<dynamic>(
            shopDomain,
            $"custom_collections/{collectionId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateCustomCollection",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.custom_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { CustomCollection = dto });
    }

    public async Task<Result<bool>> DeleteCustomCollectionAsync(string shopDomain, string collectionId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"custom_collections/{collectionId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteCustomCollection",
            RequestBody = JsonConvert.SerializeObject(new { collectionId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    // ---- Smart Collections ----

    public async Task<Result<ListCollectionsResponse>> ListSmartCollectionsAsync(string shopDomain, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListCollectionsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListCollectionsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var raw = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "smart_collections.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListSmartCollections",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<ListCollectionsResponse>.Fail("No response from Shopify API.");

        var list = JsonConvert.DeserializeObject<List<CollectionDto>>(JsonConvert.SerializeObject(raw?.smart_collections)) ?? new List<CollectionDto>();
        return Result<ListCollectionsResponse>.Ok(new ListCollectionsResponse { SmartCollections = list });
    }

    public async Task<Result<CollectionResponse>> GetSmartCollectionAsync(string shopDomain, string collectionId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var raw = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"smart_collections/{collectionId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetSmartCollection",
            RequestBody = JsonConvert.SerializeObject(new { collectionId }),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.smart_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { SmartCollection = dto });
    }

    public async Task<Result<int>> CountSmartCollectionsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "smart_collections/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountSmartCollections",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<CollectionResponse>> CreateSmartCollectionAsync(string shopDomain, CreateCollectionRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { smart_collection = request };

        var raw = await _apiClient.PostAsync<dynamic>(
            shopDomain,
            "smart_collections.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateSmartCollection",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.smart_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { SmartCollection = dto });
    }

    public async Task<Result<CollectionResponse>> UpdateSmartCollectionAsync(string shopDomain, string collectionId, UpdateCollectionRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<CollectionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<CollectionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { smart_collection = request };

        var raw = await _apiClient.PutAsync<dynamic>(
            shopDomain,
            $"smart_collections/{collectionId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateSmartCollection",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(raw),
            IsSuccess = raw != null,
            StatusCode = raw != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (raw == null)
            return Result<CollectionResponse>.Fail("No response from Shopify API.");

        var dto = JsonConvert.DeserializeObject<CollectionDto>(JsonConvert.SerializeObject(raw?.smart_collection));
        return Result<CollectionResponse>.Ok(new CollectionResponse { SmartCollection = dto });
    }

    public async Task<Result<bool>> DeleteSmartCollectionAsync(string shopDomain, string collectionId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"smart_collections/{collectionId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteSmartCollection",
            RequestBody = JsonConvert.SerializeObject(new { collectionId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}

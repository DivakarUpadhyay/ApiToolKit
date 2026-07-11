using ApiToolKit.Application.DTOs.Facebook.Catalog;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookCatalogService : IFacebookCatalogService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookCatalogRepository _catalogRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookCatalogService(IFacebookAuthRepository authRepo, IFacebookCatalogRepository catalogRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _catalogRepo = catalogRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CreateCatalogResponse>> CreateCatalogAsync(CreateCatalogRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateCatalogResponse>.Fail("Facebook configuration not found.");

        var body = new
        {
            name = request.Name,
            vertical = request.VerticalType
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.BusinessId}/owned_product_catalogs", body, accessToken);
        if (response == null) return Result<CreateCatalogResponse>.Fail("No response from Facebook API.");

        string catalogId = response?.id?.ToString() ?? string.Empty;

        await _catalogRepo.SaveCatalogAsync(new FacebookCatalog
        {
            CatalogId = catalogId,
            BusinessId = request.BusinessId,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _catalogRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateCatalog",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateCatalogResponse>.Ok(new CreateCatalogResponse { Id = catalogId });
    }

    public async Task<Result<CatalogResponse>> GetCatalogAsync(string catalogId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CatalogResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,business,vertical,product_count,created_time",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}", queryParams);
        if (response == null) return Result<CatalogResponse>.Fail("No response from Facebook API.");

        var catalog = JsonConvert.DeserializeObject<CatalogResponse>(JsonConvert.SerializeObject(response)) ?? new CatalogResponse();
        return Result<CatalogResponse>.Ok(catalog);
    }

    public async Task<Result<GetCatalogsResponse>> GetCatalogsAsync(GetCatalogsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetCatalogsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,vertical,product_count,created_time",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.BusinessId}/owned_product_catalogs", queryParams);
        if (response == null) return Result<GetCatalogsResponse>.Fail("No response from Facebook API.");

        var catalogs = JsonConvert.DeserializeObject<List<CatalogResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<CatalogResponse>();

        return Result<GetCatalogsResponse>.Ok(new GetCatalogsResponse { Data = catalogs });
    }

    public async Task<Result<bool>> DeleteCatalogAsync(string catalogId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{catalogId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _catalogRepo.DeleteCatalogAsync(catalogId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<CreateProductResponse>> CreateProductAsync(CreateProductRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateProductResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["retailer_id"] = request.RetailerId,
            ["name"] = request.Name,
            ["availability"] = request.Availability,
            ["condition"] = request.Condition
        };
        if (request.Description != null) body["description"] = request.Description;
        if (request.Url != null) body["url"] = request.Url;
        if (request.ImageUrl != null) body["image_url"] = request.ImageUrl;
        if (request.Brand != null) body["brand"] = request.Brand;
        if (request.Price != null) body["price"] = request.Price;
        if (request.Currency != null) body["currency"] = request.Currency;
        if (request.Gtin != null) body["gtin"] = request.Gtin;
        if (request.Mpn != null) body["mpn"] = request.Mpn;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.CatalogId}/products", body, accessToken);
        if (response == null) return Result<CreateProductResponse>.Fail("No response from Facebook API.");

        return Result<CreateProductResponse>.Ok(new CreateProductResponse { Id = response?.id?.ToString() ?? string.Empty });
    }

    public async Task<Result<ProductResponse>> GetProductAsync(string productId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<ProductResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,retailer_id,name,description,url,image_url,brand,price,currency,availability,condition",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{productId}", queryParams);
        if (response == null) return Result<ProductResponse>.Fail("No response from Facebook API.");

        var product = JsonConvert.DeserializeObject<ProductResponse>(JsonConvert.SerializeObject(response)) ?? new ProductResponse();
        return Result<ProductResponse>.Ok(product);
    }

    public async Task<Result<GetProductsResponse>> GetProductsAsync(GetProductsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetProductsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.Filter != null) queryParams["filter"] = request.Filter;
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.CatalogId}/products", queryParams);
        if (response == null) return Result<GetProductsResponse>.Fail("No response from Facebook API.");

        var products = JsonConvert.DeserializeObject<List<ProductResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<ProductResponse>();

        return Result<GetProductsResponse>.Ok(new GetProductsResponse { Data = products });
    }

    public async Task<Result<bool>> UpdateProductAsync(UpdateProductRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Name != null) body["name"] = request.Name;
        if (request.Description != null) body["description"] = request.Description;
        if (request.Price != null) body["price"] = request.Price;
        if (request.Availability != null) body["availability"] = request.Availability;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.ProductId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteProductAsync(string productId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{productId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<ProductSetResponse>> CreateProductSetAsync(CreateProductSetRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<ProductSetResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?> { ["name"] = request.Name };
        if (request.Filter != null) body["filter"] = request.Filter;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.CatalogId}/product_sets", body, accessToken);
        if (response == null) return Result<ProductSetResponse>.Fail("No response from Facebook API.");

        return Result<ProductSetResponse>.Ok(new ProductSetResponse
        {
            Id = response?.id?.ToString() ?? string.Empty,
            Name = request.Name,
            Filter = request.Filter
        });
    }

    public async Task<Result<List<ProductSetResponse>>> GetProductSetsAsync(string catalogId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<ProductSetResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,filter,product_count",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}/product_sets", queryParams);
        if (response == null) return Result<List<ProductSetResponse>>.Fail("No response from Facebook API.");

        var sets = JsonConvert.DeserializeObject<List<ProductSetResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<ProductSetResponse>();

        return Result<List<ProductSetResponse>>.Ok(sets);
    }
}

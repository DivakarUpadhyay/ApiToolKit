using ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookWhatsAppCatalogService : IFacebookWhatsAppCatalogService
{
    private readonly IWhatsAppApiClient _apiClient;
    private readonly IFacebookWhatsAppCatalogRepository _repo;

    public FacebookWhatsAppCatalogService(IWhatsAppApiClient apiClient, IFacebookWhatsAppCatalogRepository repo)
    {
        _apiClient = apiClient;
        _repo = repo;
    }

    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null)
    {
        await _repo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = apiName,
            RequestBody = request ?? string.Empty,
            ResponseBody = response ?? string.Empty,
            IsSuccess = success,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<WabaCatalogsResponse>> GetCatalogsAsync(string wabaId, string accessToken)
    {
        try
        {
            // Catalogs connected to the WABA.
            var response = await _apiClient.GetAsync<dynamic>($"/{wabaId}/product_catalogs", accessToken, new Dictionary<string, string> { ["fields"] = "id,name,vertical" });
            if (response == null) return Result<WabaCatalogsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalogsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCatalogs", true, 200, wabaId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalogsResponse>.Ok(result) : Result<WabaCatalogsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCatalogs", false, 500, wabaId, ex.Message);
            return Result<WabaCatalogsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalog>> CreateCatalogAsync(string businessId, CreateWabaCatalogRequest request, string accessToken)
    {
        try
        {
            // Catalogs are owned by the Meta Business portfolio, not the WABA; connect it to the WABA afterwards.
            var body = new { name = request.Name, vertical = request.Vertical };
            var response = await _apiClient.PostAsync<dynamic>($"/{businessId}/owned_product_catalogs", accessToken, body);
            if (response == null) return Result<WabaCatalog>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalog>(JsonConvert.SerializeObject(response));
            await LogAsync("CreateCatalog", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalog>.Ok(result) : Result<WabaCatalog>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("CreateCatalog", false, 500, businessId, ex.Message);
            return Result<WabaCatalog>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalog>> GetCatalogAsync(string catalogId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}", accessToken, new Dictionary<string, string> { ["fields"] = "id,name,vertical" });
            if (response == null) return Result<WabaCatalog>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalog>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCatalog", true, 200, catalogId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalog>.Ok(result) : Result<WabaCatalog>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCatalog", false, 500, catalogId, ex.Message);
            return Result<WabaCatalog>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalogProductsResponse>> GetCatalogProductsAsync(string catalogId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}/products", accessToken, new Dictionary<string, string> { ["fields"] = "id,name,description,price,currency,availability,condition,image_url,retailer_id,url" });
            if (response == null) return Result<WabaCatalogProductsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalogProductsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCatalogProducts", true, 200, catalogId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalogProductsResponse>.Ok(result) : Result<WabaCatalogProductsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCatalogProducts", false, 500, catalogId, ex.Message);
            return Result<WabaCatalogProductsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalogProduct>> AddCatalogProductAsync(string catalogId, CreateWabaCatalogProductRequest request, string accessToken)
    {
        try
        {
            var body = new { name = request.Name, description = request.Description, price = request.Price, currency = request.Currency, availability = request.Availability, condition = request.Condition, image_url = request.ImageUrl, brand = request.Brand, retailer_id = request.RetailerId, url = request.Url };
            var response = await _apiClient.PostAsync<dynamic>($"/{catalogId}/products", accessToken, body);
            if (response == null) return Result<WabaCatalogProduct>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalogProduct>(JsonConvert.SerializeObject(response));
            await LogAsync("AddCatalogProduct", true, 200, catalogId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalogProduct>.Ok(result) : Result<WabaCatalogProduct>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("AddCatalogProduct", false, 500, catalogId, ex.Message);
            return Result<WabaCatalogProduct>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalogProduct>> GetCatalogProductAsync(string productId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{productId}", accessToken, new Dictionary<string, string> { ["fields"] = "id,name,description,price,currency,availability,condition,image_url,retailer_id,url" });
            if (response == null) return Result<WabaCatalogProduct>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<WabaCatalogProduct>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCatalogProduct", true, 200, productId, JsonConvert.SerializeObject(response));
            return result != null ? Result<WabaCatalogProduct>.Ok(result) : Result<WabaCatalogProduct>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCatalogProduct", false, 500, productId, ex.Message);
            return Result<WabaCatalogProduct>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteCatalogProductAsync(string productId, string accessToken)
    {
        try
        {
            var response = await _apiClient.DeleteAsync<dynamic>($"/{productId}", accessToken);
            var success = response != null;
            await LogAsync("DeleteCatalogProduct", success, success ? 200 : 400, productId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete catalog product.");
        }
        catch (Exception ex)
        {
            await LogAsync("DeleteCatalogProduct", false, 500, productId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<WabaCatalogsResponse>> GetBusinessCatalogsAsync(string businessId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<WabaCatalogsResponse>($"/{businessId}/owned_product_catalogs", accessToken, new Dictionary<string, string> { ["fields"] = "id,name,vertical" });
            await LogAsync("GetBusinessCatalogs", true, 200, businessId, JsonConvert.SerializeObject(response));
            return response != null ? Result<WabaCatalogsResponse>.Ok(response) : Result<WabaCatalogsResponse>.Fail("No response from API.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetBusinessCatalogs", false, 500, businessId, ex.Message);
            return Result<WabaCatalogsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> ConnectCatalogAsync(string wabaId, string catalogId, string accessToken)
    {
        try
        {
            await _apiClient.PostAsync<object>($"/{wabaId}/product_catalogs", accessToken, new { catalog_id = catalogId });
            await LogAsync("ConnectCatalog", true, 200, $"{wabaId}:{catalogId}");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("ConnectCatalog", false, 500, $"{wabaId}:{catalogId}", ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DisconnectCatalogAsync(string wabaId, string catalogId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{wabaId}/product_catalogs", accessToken, body: new { catalog_id = catalogId });
            await LogAsync("DisconnectCatalog", true, 200, $"{wabaId}:{catalogId}");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("DisconnectCatalog", false, 500, $"{wabaId}:{catalogId}", ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateCatalogProductAsync(string productId, UpdateWabaCatalogProductRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>();
            if (request.Name != null) body["name"] = request.Name;
            if (request.Description != null) body["description"] = request.Description;
            if (request.Price != null) body["price"] = request.Price;
            if (request.Availability != null) body["availability"] = request.Availability;
            if (request.ImageUrl != null) body["image_url"] = request.ImageUrl;
            await _apiClient.PostAsync<object>($"/{productId}", accessToken, body);
            await LogAsync("UpdateCatalogProduct", true, 200, productId);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateCatalogProduct", false, 500, productId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }
}

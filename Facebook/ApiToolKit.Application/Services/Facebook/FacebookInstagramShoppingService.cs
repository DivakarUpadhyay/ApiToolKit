using ApiToolKit.Application.DTOs.Facebook.InstagramShopping;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookInstagramShoppingService : IFacebookInstagramShoppingService
{
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookInstagramShoppingRepository _repo;

    public FacebookInstagramShoppingService(IFacebookApiClient apiClient, IFacebookInstagramShoppingRepository repo)
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

    public async Task<Result<IgProductCatalog>> GetLinkedCatalogAsync(string igUserId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/product_catalog?fields=id,name,vertical", accessToken: accessToken);
            if (response == null) return Result<IgProductCatalog>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgProductCatalog>(JsonConvert.SerializeObject(response));
            await LogAsync("GetLinkedCatalog", true, 200, igUserId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgProductCatalog>.Ok(result) : Result<IgProductCatalog>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetLinkedCatalog", false, 500, igUserId, ex.Message);
            return Result<IgProductCatalog>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> TagProductsOnMediaAsync(string mediaId, TagProductsRequest request, string accessToken)
    {
        try
        {
            var body = new { updated_tags = request.Tags.Select(t => new { product_id = t.ProductId, x = t.X, y = t.Y }) };
            var response = await _apiClient.PostAsync<dynamic>($"/{mediaId}/product_tags", body, accessToken: accessToken);
            var success = response != null;
            await LogAsync("TagProductsOnMedia", success, success ? 200 : 400, mediaId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to tag products.");
        }
        catch (Exception ex)
        {
            await LogAsync("TagProductsOnMedia", false, 500, mediaId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<IgProductTagsResponse>> GetProductTagsOnMediaAsync(string mediaId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{mediaId}/product_tags?fields=product_id,merchant_id,name,image_url,price_string,currency_code", accessToken: accessToken);
            if (response == null) return Result<IgProductTagsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgProductTagsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetProductTagsOnMedia", true, 200, mediaId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgProductTagsResponse>.Ok(result) : Result<IgProductTagsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetProductTagsOnMedia", false, 500, mediaId, ex.Message);
            return Result<IgProductTagsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteProductTagsOnMediaAsync(string mediaId, string accessToken)
    {
        try
        {
            var response = await _apiClient.DeleteAsync<dynamic>($"/{mediaId}/product_tags", accessToken: accessToken);
            var success = response != null;
            await LogAsync("DeleteProductTagsOnMedia", success, success ? 200 : 400, mediaId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete product tags.");
        }
        catch (Exception ex)
        {
            await LogAsync("DeleteProductTagsOnMedia", false, 500, mediaId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<IgTaggedMediaResponse>> GetTaggedProductMediaAsync(string igUserId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/product_tagged_media?fields=id,media_type,media_url,timestamp,permalink", accessToken: accessToken);
            if (response == null) return Result<IgTaggedMediaResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgTaggedMediaResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetTaggedProductMedia", true, 200, igUserId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgTaggedMediaResponse>.Ok(result) : Result<IgTaggedMediaResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetTaggedProductMedia", false, 500, igUserId, ex.Message);
            return Result<IgTaggedMediaResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> AppealProductAsync(string igUserId, ProductAppealRequest request, string accessToken)
    {
        try
        {
            var body = new { product_id = request.ProductId, appeal_reason = request.AppealReason };
            var response = await _apiClient.PostAsync<dynamic>($"/{igUserId}/product_appeal", body, accessToken: accessToken);
            var success = response != null;
            await LogAsync("AppealProduct", success, success ? 200 : 400, igUserId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to submit product appeal.");
        }
        catch (Exception ex)
        {
            await LogAsync("AppealProduct", false, 500, igUserId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<IgShoppingEligibility>> GetShoppingEligibilityAsync(string igUserId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}?fields=shopping_product_tag_eligibility", accessToken: accessToken);
            if (response == null) return Result<IgShoppingEligibility>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgShoppingEligibility>(JsonConvert.SerializeObject(response));
            await LogAsync("GetShoppingEligibility", true, 200, igUserId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgShoppingEligibility>.Ok(result) : Result<IgShoppingEligibility>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetShoppingEligibility", false, 500, igUserId, ex.Message);
            return Result<IgShoppingEligibility>.Fail(ex.Message);
        }
    }

    public async Task<Result<IgCatalogProductsResponse>> GetCatalogProductsAsync(string catalogId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}/products?fields=id,name,description,price_amount,currency,availability,image_url,retailer_id,review_status", accessToken: accessToken);
            if (response == null) return Result<IgCatalogProductsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgCatalogProductsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCatalogProducts", true, 200, catalogId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgCatalogProductsResponse>.Ok(result) : Result<IgCatalogProductsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCatalogProducts", false, 500, catalogId, ex.Message);
            return Result<IgCatalogProductsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<IgShopsResponse>> GetIgShopsAsync(string igUserId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/shops?fields=id,name,status", accessToken: accessToken);
            if (response == null) return Result<IgShopsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<IgShopsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetIgShops", true, 200, igUserId, JsonConvert.SerializeObject(response));
            return result != null ? Result<IgShopsResponse>.Ok(result) : Result<IgShopsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetIgShops", false, 500, igUserId, ex.Message);
            return Result<IgShopsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AvailableCatalogsResponse>> GetAvailableCatalogsAsync(string igUserId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/available_catalogs?fields=catalog_id,catalog_name,catalog_source", accessToken: accessToken);
            if (response == null) return Result<AvailableCatalogsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<AvailableCatalogsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetAvailableCatalogs", true, 200, igUserId, JsonConvert.SerializeObject(response));
            return result != null ? Result<AvailableCatalogsResponse>.Ok(result) : Result<AvailableCatalogsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetAvailableCatalogs", false, 500, igUserId, ex.Message);
            return Result<AvailableCatalogsResponse>.Fail(ex.Message);
        }
    }
}

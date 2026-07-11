using ApiToolKit.Application.DTOs.Shopee.Product;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeProductService : IShopeeProductService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeProductRepository _productRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeProductService(IShopeeAuthRepository authRepo, IShopeeProductRepository productRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _productRepo = productRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<ItemListResponse>> GetItemListAsync(GetItemListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<ItemListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["offset"] = request.Offset.ToString(),
            ["page_size"] = request.PageSize.ToString(),
            ["item_status"] = request.ItemStatus
        };
        if (request.UpdateTimeFrom > 0) queryParams["update_time_from"] = request.UpdateTimeFrom.ToString();
        if (request.UpdateTimeTo > 0) queryParams["update_time_to"] = request.UpdateTimeTo.ToString();

        var response = await _apiClient.GetAsync<ItemListResponse>("/api/v2/product/get_item_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<ItemListResponse>.Fail("No response from Shopee API.");
        if (!string.IsNullOrEmpty(response.Error)) return Result<ItemListResponse>.Fail(response.Message);
        return Result<ItemListResponse>.Ok(response);
    }

    public async Task<Result<List<ItemResponse>>> GetItemBaseInfoAsync(GetItemBaseInfoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<List<ItemResponse>>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["item_id_list"] = string.Join(",", request.ItemIdList),
            ["need_tax_info"] = request.NeedTaxInfo.ToString().ToLower(),
            ["need_complaint_policy"] = request.NeedComplaintPolicy.ToString().ToLower()
        };

        var response = await _apiClient.GetAsync<dynamic>("/api/v2/product/get_item_base_info", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<List<ItemResponse>>.Fail("No response from Shopee API.");

        var items = JsonConvert.DeserializeObject<List<ItemResponse>>(JsonConvert.SerializeObject(response?.response?.item_list ?? new List<object>()));
        return Result<List<ItemResponse>>.Ok(items ?? new List<ItemResponse>());
    }

    public async Task<Result<ItemResponse>> AddItemAsync(AddItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<ItemResponse>.Fail("Access token not found.");

        var body = new
        {
            original_price = request.OriginalPrice,
            description = request.Description,
            item_name = request.ItemName,
            category_id = request.CategoryId,
            condition = request.Condition,
            stock = new[] { new { model_id = 0, seller_stock = new[] { new { stock = request.Stock } } } },
            weight = request.Weight,
            image = new { image_url_list = request.ImageUrlList },
            attribute_list = request.AttributeList,
            logistics_info = request.Logistics,
            dimension = request.Dimension
        };

        var response = await _apiClient.PostAsync<ItemResponse>("/api/v2/product/add_item", body, accessToken, request.ShopId);
        if (response == null) return Result<ItemResponse>.Fail("No response from Shopee API.");
        if (!string.IsNullOrEmpty(response.Error)) return Result<ItemResponse>.Fail(response.Message);

        await _productRepo.SaveProductAsync(request.ShopId, response.ItemId, request.ItemName, request.Description,
            request.CategoryId, request.Condition, "NORMAL", decimal.Parse(request.OriginalPrice),
            request.Stock, JsonConvert.SerializeObject(request.ImageUrlList), "", JsonConvert.SerializeObject(request.AttributeList));

        return Result<ItemResponse>.Ok(response);
    }

    public async Task<Result<ItemResponse>> UpdateItemAsync(UpdateItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<ItemResponse>.Fail("Access token not found.");

        var body = new
        {
            item_id = request.ItemId,
            description = request.Description,
            item_name = request.ItemName,
            category_id = request.CategoryId,
            condition = request.Condition,
            image = request.ImageUrlList != null ? new { image_url_list = request.ImageUrlList } : null
        };

        var response = await _apiClient.PostAsync<ItemResponse>("/api/v2/product/update_item", body, accessToken, request.ShopId);
        if (response == null) return Result<ItemResponse>.Fail("No response from Shopee API.");
        return Result<ItemResponse>.Ok(response);
    }

    public async Task<Result<bool>> UpdateItemPriceAsync(UpdatePriceRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new
        {
            item_id = request.ItemId,
            price_list = new[] { new { model_id = request.ModelId, original_price = request.Price } }
        };

        await _apiClient.PostAsync<dynamic>("/api/v2/product/update_price", body, accessToken, request.ShopId);
        await _productRepo.UpdateProductPriceAsync(request.ShopId, request.ItemId, request.ModelId, request.Price);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> UpdateItemStockAsync(UpdateStockRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new
        {
            item_id = request.ItemId,
            stock_list = new[] { new { model_id = request.ModelId, seller_stock = new[] { new { location_id = request.LocationId.ToString(), stock = request.Stock } } } }
        };

        await _apiClient.PostAsync<dynamic>("/api/v2/product/update_stock", body, accessToken, request.ShopId);
        await _productRepo.UpdateProductStockAsync(request.ShopId, request.ItemId, request.ModelId, request.Stock);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteItemAsync(DeleteItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { item_id_list = request.ItemIdList };
        await _apiClient.PostAsync<dynamic>("/api/v2/product/delete_item", body, accessToken, request.ShopId);
        foreach (var itemId in request.ItemIdList)
            await _productRepo.DeleteProductAsync(request.ShopId, itemId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetCategoryAsync(GetCategoryRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["language"] = request.Language };
        var response = await _apiClient.GetAsync<object>("/api/v2/product/get_category", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetAttributesAsync(GetAttributesRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["category_id"] = request.CategoryId.ToString(), ["language"] = request.Language };
        var response = await _apiClient.GetAsync<object>("/api/v2/product/get_attributes", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetBrandsAsync(long shopId, long categoryId, string language)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["category_id"] = categoryId.ToString(), ["language"] = language, ["offset"] = "0", ["page_size"] = "100", ["status"] = "1" };
        var response = await _apiClient.GetAsync<object>("/api/v2/product/get_brand_list", queryParams, accessToken, shopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetModelListAsync(long shopId, long itemId)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["item_id"] = itemId.ToString() };
        var response = await _apiClient.GetAsync<object>("/api/v2/product/get_model_list", queryParams, accessToken, shopId);
        if (response == null) return Result<object>.Fail("No response from Shopee API.");
        return Result<object>.Ok(response);
    }

    public async Task<Result<bool>> InitTierVariationAsync(long shopId, long itemId, object tierVariation)
    {
        var accessToken = await GetAccessTokenAsync(shopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { item_id = itemId, tier_variation = tierVariation };
        await _apiClient.PostAsync<dynamic>("/api/v2/product/init_tier_variation", body, accessToken, shopId);
        return Result<bool>.Ok(true);
    }
}



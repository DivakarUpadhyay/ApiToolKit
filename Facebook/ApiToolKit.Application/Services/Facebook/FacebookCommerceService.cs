using ApiToolKit.Application.DTOs.Facebook.Commerce;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookCommerceService : IFacebookCommerceService
{
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookCommerceRepository _repo;
    public FacebookCommerceService(IFacebookApiClient apiClient, IFacebookCommerceRepository repo)
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

    public async Task<Result<FbShopsResponse>> GetShopsAsync(string pageId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/shops?fields=id,name,description,status", accessToken: accessToken);
            if (response == null) return Result<FbShopsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FbShopsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetShops", true, 200, pageId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FbShopsResponse>.Ok(result) : Result<FbShopsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetShops", false, 500, pageId, ex.Message);
            return Result<FbShopsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<FbShop>> GetShopAsync(string shopId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{shopId}?fields=id,name,description,status", accessToken: accessToken);
            if (response == null) return Result<FbShop>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FbShop>(JsonConvert.SerializeObject(response));
            await LogAsync("GetShop", true, 200, shopId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FbShop>.Ok(result) : Result<FbShop>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetShop", false, 500, shopId, ex.Message);
            return Result<FbShop>.Fail(ex.Message);
        }
    }

    public async Task<Result<CommerceOrdersResponse>> GetOrdersAsync(string pageId, string accessToken, string? state = null)
    {
        try
        {
            var fields = "id,order_status,buyer_details,items{id,name,quantity,price_amount,currency,retailer_id},shipping_address,estimated_payment_details,created,last_updated";
            var path = $"/{pageId}/commerce_orders?fields={fields}";
            if (state != null) path += $"&state={state}";
            var response = await _apiClient.GetAsync<dynamic>(path, accessToken: accessToken);
            if (response == null) return Result<CommerceOrdersResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CommerceOrdersResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOrders", true, 200, pageId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CommerceOrdersResponse>.Ok(result) : Result<CommerceOrdersResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetOrders", false, 500, pageId, ex.Message);
            return Result<CommerceOrdersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CommerceOrder>> GetOrderAsync(string orderId, string accessToken)
    {
        try
        {
            var fields = "id,order_status,buyer_details,items{id,name,quantity,price_amount,currency,retailer_id},shipping_address,estimated_payment_details,created,last_updated";
            var response = await _apiClient.GetAsync<dynamic>($"/{orderId}?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<CommerceOrder>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CommerceOrder>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOrder", true, 200, orderId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CommerceOrder>.Ok(result) : Result<CommerceOrder>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetOrder", false, 500, orderId, ex.Message);
            return Result<CommerceOrder>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> AcknowledgeOrderAsync(string orderId, AcknowledgeOrderRequest request, string accessToken)
    {
        try
        {
            var body = new { merchant_order_reference = request.MerchantOrderReference };
            var response = await _apiClient.PostAsync<dynamic>($"/{orderId}/acknowledge_order", body, accessToken);
            await LogAsync("AcknowledgeOrder", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to acknowledge order.");
        }
        catch (Exception ex)
        {
            await LogAsync("AcknowledgeOrder", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> AddShipmentAsync(string orderId, AddShipmentRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                tracking_info = new
                {
                    tracking_number = request.TrackingNumber,
                    carrier = request.CarrierName,
                    shipping_method = request.ShippingMethod
                },
                items = request.Items?.Select(i => new { retailer_id = i.RetailerId, quantity = i.Quantity })
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{orderId}/shipments", body, accessToken);
            await LogAsync("AddShipment", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to add shipment.");
        }
        catch (Exception ex)
        {
            await LogAsync("AddShipment", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> CancelOrderAsync(string orderId, CancelOrderRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                reason_code = request.ReasonCode,
                cancel_reason = new { description = request.ReasonDescription }
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{orderId}/cancellations", body, accessToken);
            await LogAsync("CancelOrder", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to cancel order.");
        }
        catch (Exception ex)
        {
            await LogAsync("CancelOrder", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> RefundOrderAsync(string orderId, RefundOrderRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                refund_amount = new { amount = request.RefundAmount, currency = request.Currency },
                reason_text = request.ReasonText
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{orderId}/refunds", body, accessToken);
            await LogAsync("RefundOrder", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to refund order.");
        }
        catch (Exception ex)
        {
            await LogAsync("RefundOrder", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<FulfillmentsResponse>> GetFulfillmentsAsync(string orderId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{orderId}/fulfillments", accessToken: accessToken);
            if (response == null) return Result<FulfillmentsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FulfillmentsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetFulfillments", true, 200, orderId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FulfillmentsResponse>.Ok(result) : Result<FulfillmentsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetFulfillments", false, 500, orderId, ex.Message);
            return Result<FulfillmentsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ReturnsResponse>> GetReturnsAsync(string orderId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{orderId}/returns", accessToken: accessToken);
            if (response == null) return Result<ReturnsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ReturnsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetReturns", true, 200, orderId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetReturns", false, 500, orderId, ex.Message);
            return Result<ReturnsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CommerceAccount>> GetCommerceAccountAsync(string pageId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/commerce_account?fields=id,name", accessToken: accessToken);
            if (response == null) return Result<CommerceAccount>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CommerceAccount>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCommerceAccount", true, 200, pageId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CommerceAccount>.Ok(result) : Result<CommerceAccount>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCommerceAccount", false, 500, pageId, ex.Message);
            return Result<CommerceAccount>.Fail(ex.Message);
        }
    }

    public async Task<Result<CommerceSettings>> GetCommerceSettingsAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/commerce_settings", accessToken: accessToken);
            if (response == null) return Result<CommerceSettings>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CommerceSettings>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCommerceSettings", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CommerceSettings>.Ok(result) : Result<CommerceSettings>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCommerceSettings", false, 500, commerceAccountId, ex.Message);
            return Result<CommerceSettings>.Fail(ex.Message);
        }
    }

    public async Task<Result<TaxSettings>> GetTaxSettingsAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/tax_settings", accessToken: accessToken);
            if (response == null) return Result<TaxSettings>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<TaxSettings>(JsonConvert.SerializeObject(response));
            await LogAsync("GetTaxSettings", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<TaxSettings>.Ok(result) : Result<TaxSettings>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetTaxSettings", false, 500, commerceAccountId, ex.Message);
            return Result<TaxSettings>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateTaxSettingsAsync(string commerceAccountId, UpdateTaxSettingsRequest request, string accessToken)
    {
        try
        {
            var body = new { disable_payment_tax = request.DisablePaymentTax };
            var response = await _apiClient.PostAsync<dynamic>($"/{commerceAccountId}/tax_settings", body, accessToken);
            await LogAsync("UpdateTaxSettings", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update tax settings.");
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateTaxSettings", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<ReturnSettings>> GetReturnSettingsAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/return_settings", accessToken: accessToken);
            if (response == null) return Result<ReturnSettings>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ReturnSettings>(JsonConvert.SerializeObject(response));
            await LogAsync("GetReturnSettings", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ReturnSettings>.Ok(result) : Result<ReturnSettings>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetReturnSettings", false, 500, commerceAccountId, ex.Message);
            return Result<ReturnSettings>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateReturnSettingsAsync(string commerceAccountId, UpdateReturnSettingsRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                return_window_days = request.ReturnWindowDays,
                is_enabled = request.IsEnabled,
                return_shipping_option = request.ReturnShippingOption
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{commerceAccountId}/return_settings", body, accessToken);
            await LogAsync("UpdateReturnSettings", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update return settings.");
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateReturnSettings", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<ShippingProfilesResponse>> GetShippingProfilesAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/shipping_profiles?fields=id,name,origin_country", accessToken: accessToken);
            if (response == null) return Result<ShippingProfilesResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ShippingProfilesResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetShippingProfiles", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ShippingProfilesResponse>.Ok(result) : Result<ShippingProfilesResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetShippingProfiles", false, 500, commerceAccountId, ex.Message);
            return Result<ShippingProfilesResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ShopCollectionsResponse>> GetShopCollectionsAsync(string shopId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{shopId}/collections?fields=id,name", accessToken: accessToken);
            if (response == null) return Result<ShopCollectionsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ShopCollectionsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetShopCollections", true, 200, shopId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ShopCollectionsResponse>.Ok(result) : Result<ShopCollectionsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetShopCollections", false, 500, shopId, ex.Message);
            return Result<ShopCollectionsResponse>.Fail(ex.Message);
        }
    }

    // ── Shop setup ────────────────────────────────────────────────────────────

    public async Task<Result<object>> CreateShopAsync(CreateShopRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["catalog_id"] = request.CatalogId };
            if (request.Name != null) body["name"] = request.Name;
            if (request.Description != null) body["description"] = request.Description;
            var response = await _apiClient.PostAsync<object>($"/{request.PageId}/shops", body, accessToken);
            await LogAsync("CreateShop", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<object>.Ok(response) : Result<object>.Fail("Failed to create shop.");
        }
        catch (Exception ex)
        {
            await LogAsync("CreateShop", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<object>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateShopAsync(UpdateShopRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>();
            if (request.Name != null) body["name"] = request.Name;
            if (request.Description != null) body["description"] = request.Description;
            if (request.IsEnabled.HasValue) body["is_enabled"] = request.IsEnabled.Value;
            await _apiClient.PostAsync<object>($"/{request.ShopId}", body, accessToken);
            await LogAsync("UpdateShop", true, 200, JsonConvert.SerializeObject(request), "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateShop", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    // ── Catalog connection ─────────────────────────────────────────────────────

    public async Task<Result<ConnectedCatalogsResponse>> GetConnectedCatalogsAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/product_catalogs?fields=id,name,vertical,product_count", accessToken: accessToken);
            if (response == null) return Result<ConnectedCatalogsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ConnectedCatalogsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetConnectedCatalogs", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ConnectedCatalogsResponse>.Ok(result) : Result<ConnectedCatalogsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetConnectedCatalogs", false, 500, commerceAccountId, ex.Message);
            return Result<ConnectedCatalogsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> ConnectCatalogAsync(ConnectCatalogRequest request, string accessToken)
    {
        try
        {
            var body = new { catalog_id = request.CatalogId };
            await _apiClient.PostAsync<object>($"/{request.CommerceAccountId}/product_catalogs", body, accessToken);
            await LogAsync("ConnectCatalog", true, 200, JsonConvert.SerializeObject(request), "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("ConnectCatalog", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DisconnectCatalogAsync(string commerceAccountId, string catalogId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{commerceAccountId}/product_catalogs?catalog_id={catalogId}", accessToken);
            await LogAsync("DisconnectCatalog", true, 200, $"{commerceAccountId}/{catalogId}", "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("DisconnectCatalog", false, 500, $"{commerceAccountId}/{catalogId}", ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    // ── Collections ────────────────────────────────────────────────────────────

    public async Task<Result<List<CollectionResponse>>> GetCollectionsAsync(string commerceAccountId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{commerceAccountId}/collections?fields=id,name,description,product_set_id,cover_image_url", accessToken: accessToken);
            if (response == null) return Result<List<CollectionResponse>>.Fail("No response from API.");
            var list = JsonConvert.DeserializeObject<List<CollectionResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<CollectionResponse>();
            await LogAsync("GetCollections", true, 200, commerceAccountId, JsonConvert.SerializeObject(response));
            return Result<List<CollectionResponse>>.Ok(list);
        }
        catch (Exception ex)
        {
            await LogAsync("GetCollections", false, 500, commerceAccountId, ex.Message);
            return Result<List<CollectionResponse>>.Fail(ex.Message);
        }
    }

    public async Task<Result<object>> CreateCollectionAsync(CreateCollectionRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["name"] = request.Name };
            if (request.Description != null) body["description"] = request.Description;
            if (request.ProductSetId != null) body["product_set_id"] = request.ProductSetId;
            if (request.CoverImageUrl != null) body["cover_image_url"] = request.CoverImageUrl;
            var response = await _apiClient.PostAsync<object>($"/{request.CommerceAccountId}/collections", body, accessToken);
            await LogAsync("CreateCollection", response != null, response != null ? 200 : 400, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return response != null ? Result<object>.Ok(response) : Result<object>.Fail("Failed to create collection.");
        }
        catch (Exception ex)
        {
            await LogAsync("CreateCollection", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<object>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateCollectionAsync(UpdateCollectionRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>();
            if (request.Name != null) body["name"] = request.Name;
            if (request.Description != null) body["description"] = request.Description;
            if (request.CoverImageUrl != null) body["cover_image_url"] = request.CoverImageUrl;
            await _apiClient.PostAsync<object>($"/{request.CollectionId}", body, accessToken);
            await LogAsync("UpdateCollection", true, 200, JsonConvert.SerializeObject(request), "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateCollection", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteCollectionAsync(string collectionId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{collectionId}", accessToken);
            await LogAsync("DeleteCollection", true, 200, collectionId, "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("DeleteCollection", false, 500, collectionId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<CollectionProductsResponse>> GetCollectionProductsAsync(string collectionId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{collectionId}/products?fields=id,name,price,image_url,availability,retailer_id", accessToken: accessToken);
            if (response == null) return Result<CollectionProductsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CollectionProductsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetCollectionProducts", true, 200, collectionId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CollectionProductsResponse>.Ok(result) : Result<CollectionProductsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetCollectionProducts", false, 500, collectionId, ex.Message);
            return Result<CollectionProductsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> AddProductToCollectionAsync(AddProductToCollectionRequest request, string accessToken)
    {
        try
        {
            var body = new { product_id = request.ProductId };
            await _apiClient.PostAsync<object>($"/{request.CollectionId}/products", body, accessToken);
            await LogAsync("AddProductToCollection", true, 200, JsonConvert.SerializeObject(request), "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("AddProductToCollection", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> RemoveProductFromCollectionAsync(RemoveProductFromCollectionRequest request, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{request.CollectionId}/products?product_id={request.ProductId}", accessToken);
            await LogAsync("RemoveProductFromCollection", true, 200, JsonConvert.SerializeObject(request), "ok");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync("RemoveProductFromCollection", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }
}

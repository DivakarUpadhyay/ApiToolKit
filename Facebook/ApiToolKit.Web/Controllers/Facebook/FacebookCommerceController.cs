using ApiToolKit.Application.DTOs.Facebook.Commerce;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/commerce")]
[Produces("application/json")]
public class FacebookCommerceController : ControllerBase
{
    private readonly IFacebookCommerceService _service;
    public FacebookCommerceController(IFacebookCommerceService service) => _service = service;

    /// <summary>Get all shops for a Facebook Page.</summary>
    [HttpGet("shops/{pageId}")]
    public async Task<IActionResult> GetShops(string pageId, [FromQuery] string accessToken)
    {
        var result = await _service.GetShopsAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific shop by shop ID.</summary>
    [HttpGet("shops/detail/{shopId}")]
    public async Task<IActionResult> GetShop(string shopId, [FromQuery] string accessToken)
    {
        var result = await _service.GetShopAsync(shopId, accessToken);
        return Ok(result);
    }

    /// <summary>Get commerce orders for a Facebook Page, optionally filtered by state.</summary>
    [HttpGet("orders/{pageId}")]
    public async Task<IActionResult> GetOrders(string pageId, [FromQuery] string accessToken, [FromQuery] string? state = null)
    {
        var result = await _service.GetOrdersAsync(pageId, accessToken, state);
        return Ok(result);
    }

    /// <summary>Get details of a specific commerce order by order ID.</summary>
    [HttpGet("orders/detail/{orderId}")]
    public async Task<IActionResult> GetOrder(string orderId, [FromQuery] string accessToken)
    {
        var result = await _service.GetOrderAsync(orderId, accessToken);
        return Ok(result);
    }

    /// <summary>Acknowledge a commerce order.</summary>
    [HttpPost("orders/{orderId}/acknowledge")]
    public async Task<IActionResult> AcknowledgeOrder(string orderId, [FromBody] AcknowledgeOrderRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.AcknowledgeOrderAsync(orderId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Add a shipment to a commerce order.</summary>
    [HttpPost("orders/{orderId}/shipments")]
    public async Task<IActionResult> AddShipment(string orderId, [FromBody] AddShipmentRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.AddShipmentAsync(orderId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Cancel a commerce order.</summary>
    [HttpPost("orders/{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(string orderId, [FromBody] CancelOrderRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CancelOrderAsync(orderId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Refund a commerce order.</summary>
    [HttpPost("orders/{orderId}/refund")]
    public async Task<IActionResult> RefundOrder(string orderId, [FromBody] RefundOrderRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.RefundOrderAsync(orderId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get fulfillments for a commerce order.</summary>
    [HttpGet("orders/{orderId}/fulfillments")]
    public async Task<IActionResult> GetFulfillments(string orderId, [FromQuery] string accessToken)
    {
        var result = await _service.GetFulfillmentsAsync(orderId, accessToken);
        return Ok(result);
    }

    /// <summary>Get returns for a commerce order.</summary>
    [HttpGet("orders/{orderId}/returns")]
    public async Task<IActionResult> GetReturns(string orderId, [FromQuery] string accessToken)
    {
        var result = await _service.GetReturnsAsync(orderId, accessToken);
        return Ok(result);
    }

    /// <summary>Get the commerce account associated with a Facebook Page.</summary>
    [HttpGet("account/{pageId}")]
    public async Task<IActionResult> GetCommerceAccount(string pageId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCommerceAccountAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Get commerce settings for a commerce account.</summary>
    [HttpGet("account/{commerceAccountId}/settings")]
    public async Task<IActionResult> GetCommerceSettings(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCommerceSettingsAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Get tax settings for a commerce account.</summary>
    [HttpGet("account/{commerceAccountId}/tax-settings")]
    public async Task<IActionResult> GetTaxSettings(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetTaxSettingsAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Update tax settings for a commerce account.</summary>
    [HttpPut("account/{commerceAccountId}/tax-settings")]
    public async Task<IActionResult> UpdateTaxSettings(string commerceAccountId, [FromBody] UpdateTaxSettingsRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.UpdateTaxSettingsAsync(commerceAccountId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get return settings for a commerce account.</summary>
    [HttpGet("account/{commerceAccountId}/return-settings")]
    public async Task<IActionResult> GetReturnSettings(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetReturnSettingsAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Update return settings for a commerce account.</summary>
    [HttpPut("account/{commerceAccountId}/return-settings")]
    public async Task<IActionResult> UpdateReturnSettings(string commerceAccountId, [FromBody] UpdateReturnSettingsRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.UpdateReturnSettingsAsync(commerceAccountId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get shipping profiles for a commerce account.</summary>
    [HttpGet("account/{commerceAccountId}/shipping-profiles")]
    public async Task<IActionResult> GetShippingProfiles(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetShippingProfilesAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Get collections for a shop.</summary>
    [HttpGet("shops/{shopId}/collections")]
    public async Task<IActionResult> GetShopCollections(string shopId, [FromQuery] string accessToken)
    {
        var result = await _service.GetShopCollectionsAsync(shopId, accessToken);
        return Ok(result);
    }

    // ── Shop setup ───────────────────────────────────────────────────────────

    /// <summary>Create a new Facebook Shop linked to a Page and Catalog.</summary>
    [HttpPost("shops/create")]
    public async Task<IActionResult> CreateShop([FromBody] CreateShopRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateShopAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update shop settings (name, description, enabled state).</summary>
    [HttpPut("shops/{shopId}/update")]
    public async Task<IActionResult> UpdateShop(string shopId, [FromBody] UpdateShopRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { ShopId = shopId };
        var result = await _service.UpdateShopAsync(updatedRequest, accessToken);
        return Ok(result);
    }

    // ── Catalog connection ────────────────────────────────────────────────────

    /// <summary>List catalogs connected to a commerce account.</summary>
    [HttpGet("account/{commerceAccountId}/catalogs")]
    public async Task<IActionResult> GetConnectedCatalogs(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetConnectedCatalogsAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Connect a product catalog to a commerce account.</summary>
    [HttpPost("account/{commerceAccountId}/catalogs/connect")]
    public async Task<IActionResult> ConnectCatalog(string commerceAccountId, [FromBody] ConnectCatalogRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { CommerceAccountId = commerceAccountId };
        var result = await _service.ConnectCatalogAsync(updatedRequest, accessToken);
        return Ok(result);
    }

    /// <summary>Disconnect a product catalog from a commerce account.</summary>
    [HttpDelete("account/{commerceAccountId}/catalogs/{catalogId}")]
    public async Task<IActionResult> DisconnectCatalog(string commerceAccountId, string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.DisconnectCatalogAsync(commerceAccountId, catalogId, accessToken);
        return Ok(result);
    }

    // ── Collections ───────────────────────────────────────────────────────────

    /// <summary>List all collections for a commerce account's shop.</summary>
    [HttpGet("account/{commerceAccountId}/collections")]
    public async Task<IActionResult> GetCollections(string commerceAccountId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCollectionsAsync(commerceAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a new product collection in a shop.</summary>
    [HttpPost("account/{commerceAccountId}/collections")]
    public async Task<IActionResult> CreateCollection(string commerceAccountId, [FromBody] CreateCollectionRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { CommerceAccountId = commerceAccountId };
        var result = await _service.CreateCollectionAsync(updatedRequest, accessToken);
        return Ok(result);
    }

    /// <summary>Update a shop collection's name, description, or cover image.</summary>
    [HttpPut("collections/{collectionId}")]
    public async Task<IActionResult> UpdateCollection(string collectionId, [FromBody] UpdateCollectionRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { CollectionId = collectionId };
        var result = await _service.UpdateCollectionAsync(updatedRequest, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a shop collection.</summary>
    [HttpDelete("collections/{collectionId}")]
    public async Task<IActionResult> DeleteCollection(string collectionId, [FromQuery] string accessToken)
    {
        var result = await _service.DeleteCollectionAsync(collectionId, accessToken);
        return Ok(result);
    }

    /// <summary>List products in a shop collection.</summary>
    [HttpGet("collections/{collectionId}/products")]
    public async Task<IActionResult> GetCollectionProducts(string collectionId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCollectionProductsAsync(collectionId, accessToken);
        return Ok(result);
    }

    /// <summary>Add a product to a shop collection.</summary>
    [HttpPost("collections/{collectionId}/products")]
    public async Task<IActionResult> AddProductToCollection(string collectionId, [FromBody] AddProductToCollectionRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { CollectionId = collectionId };
        var result = await _service.AddProductToCollectionAsync(updatedRequest, accessToken);
        return Ok(result);
    }

    /// <summary>Remove a product from a shop collection.</summary>
    [HttpDelete("collections/{collectionId}/products")]
    public async Task<IActionResult> RemoveProductFromCollection(string collectionId, [FromBody] RemoveProductFromCollectionRequest request, [FromQuery] string accessToken)
    {
        var updatedRequest = request with { CollectionId = collectionId };
        var result = await _service.RemoveProductFromCollectionAsync(updatedRequest, accessToken);
        return Ok(result);
    }
}

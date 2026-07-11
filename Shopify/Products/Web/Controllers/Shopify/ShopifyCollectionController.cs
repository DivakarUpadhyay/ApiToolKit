using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Collections API (Custom and Smart)</summary>
[ApiController]
[Route("api/shopify/collections")]
[Produces("application/json")]
public class ShopifyCollectionController : ControllerBase
{
    private readonly IShopifyCollectionService _collectionService;

    public ShopifyCollectionController(IShopifyCollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    // ---- Custom Collections ----

    /// <summary>List all custom collections</summary>
    [HttpGet("custom")]
    public async Task<IActionResult> ListCustomCollectionsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.ListCustomCollectionsAsync(shopDomain, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a custom collection by ID</summary>
    [HttpGet("custom/{collectionId}")]
    public async Task<IActionResult> GetCustomCollectionAsync([FromQuery] string shopDomain, string collectionId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.GetCustomCollectionAsync(shopDomain, collectionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count custom collections</summary>
    [HttpGet("custom/count")]
    public async Task<IActionResult> CountCustomCollectionsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.CountCustomCollectionsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a custom collection</summary>
    [HttpPost("custom")]
    public async Task<IActionResult> CreateCustomCollectionAsync([FromQuery] string shopDomain, [FromBody] CreateCollectionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.CreateCustomCollectionAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a custom collection</summary>
    [HttpPut("custom/{collectionId}")]
    public async Task<IActionResult> UpdateCustomCollectionAsync([FromQuery] string shopDomain, string collectionId, [FromBody] UpdateCollectionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.UpdateCustomCollectionAsync(shopDomain, collectionId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a custom collection</summary>
    [HttpDelete("custom/{collectionId}")]
    public async Task<IActionResult> DeleteCustomCollectionAsync([FromQuery] string shopDomain, string collectionId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.DeleteCustomCollectionAsync(shopDomain, collectionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // ---- Smart Collections ----

    /// <summary>List all smart collections</summary>
    [HttpGet("smart")]
    public async Task<IActionResult> ListSmartCollectionsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.ListSmartCollectionsAsync(shopDomain, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a smart collection by ID</summary>
    [HttpGet("smart/{collectionId}")]
    public async Task<IActionResult> GetSmartCollectionAsync([FromQuery] string shopDomain, string collectionId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.GetSmartCollectionAsync(shopDomain, collectionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count smart collections</summary>
    [HttpGet("smart/count")]
    public async Task<IActionResult> CountSmartCollectionsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.CountSmartCollectionsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a smart collection</summary>
    [HttpPost("smart")]
    public async Task<IActionResult> CreateSmartCollectionAsync([FromQuery] string shopDomain, [FromBody] CreateCollectionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.CreateSmartCollectionAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a smart collection</summary>
    [HttpPut("smart/{collectionId}")]
    public async Task<IActionResult> UpdateSmartCollectionAsync([FromQuery] string shopDomain, string collectionId, [FromBody] UpdateCollectionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.UpdateSmartCollectionAsync(shopDomain, collectionId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a smart collection</summary>
    [HttpDelete("smart/{collectionId}")]
    public async Task<IActionResult> DeleteSmartCollectionAsync([FromQuery] string shopDomain, string collectionId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _collectionService.DeleteSmartCollectionAsync(shopDomain, collectionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

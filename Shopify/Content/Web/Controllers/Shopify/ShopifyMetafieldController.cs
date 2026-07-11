using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Metafields API</summary>
[ApiController]
[Route("api/shopify/metafields")]
[Produces("application/json")]
public class ShopifyMetafieldController : ControllerBase
{
    private readonly IShopifyMetafieldService _metafieldService;

    public ShopifyMetafieldController(IShopifyMetafieldService metafieldService)
    {
        _metafieldService = metafieldService;
    }

    /// <summary>List metafields (shop-level or resource-level)</summary>
    [HttpGet]
    public async Task<IActionResult> ListMetafieldsAsync([FromQuery] string shopDomain, [FromQuery] string? resource = null, [FromQuery] string? resourceId = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.ListMetafieldsAsync(shopDomain, resource, resourceId, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a metafield by ID</summary>
    [HttpGet("{metafieldId}")]
    public async Task<IActionResult> GetMetafieldAsync([FromQuery] string shopDomain, string metafieldId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.GetMetafieldAsync(shopDomain, metafieldId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count metafields (shop-level or resource-level)</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountMetafieldsAsync([FromQuery] string shopDomain, [FromQuery] string? resource = null, [FromQuery] string? resourceId = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.CountMetafieldsAsync(shopDomain, resource, resourceId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a metafield (shop-level or resource-level)</summary>
    [HttpPost]
    public async Task<IActionResult> CreateMetafieldAsync([FromQuery] string shopDomain, [FromBody] CreateMetafieldRequest request, [FromQuery] string? resource = null, [FromQuery] string? resourceId = null)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.CreateMetafieldAsync(shopDomain, request, resource, resourceId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a metafield</summary>
    [HttpPut("{metafieldId}")]
    public async Task<IActionResult> UpdateMetafieldAsync([FromQuery] string shopDomain, string metafieldId, [FromBody] UpdateMetafieldRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.UpdateMetafieldAsync(shopDomain, metafieldId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a metafield</summary>
    [HttpDelete("{metafieldId}")]
    public async Task<IActionResult> DeleteMetafieldAsync([FromQuery] string shopDomain, string metafieldId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _metafieldService.DeleteMetafieldAsync(shopDomain, metafieldId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

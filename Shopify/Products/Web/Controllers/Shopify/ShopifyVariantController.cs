using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Product Variants API</summary>
[ApiController]
[Route("api/shopify/products/{productId}/variants")]
[Produces("application/json")]
public class ShopifyVariantController : ControllerBase
{
    private readonly IShopifyVariantService _variantService;

    public ShopifyVariantController(IShopifyVariantService variantService)
    {
        _variantService = variantService;
    }

    /// <summary>List all variants for a product</summary>
    [HttpGet]
    public async Task<IActionResult> ListVariantsAsync([FromQuery] string shopDomain, string productId, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.ListVariantsAsync(shopDomain, productId, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a variant by ID</summary>
    [HttpGet("{variantId}")]
    public async Task<IActionResult> GetVariantAsync([FromQuery] string shopDomain, string productId, string variantId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.GetVariantAsync(shopDomain, variantId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count variants for a product</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountVariantsAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.CountVariantsAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a variant for a product</summary>
    [HttpPost]
    public async Task<IActionResult> CreateVariantAsync([FromQuery] string shopDomain, string productId, [FromBody] CreateVariantRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.CreateVariantAsync(shopDomain, productId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a variant</summary>
    [HttpPut("{variantId}")]
    public async Task<IActionResult> UpdateVariantAsync([FromQuery] string shopDomain, string productId, string variantId, [FromBody] UpdateVariantRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.UpdateVariantAsync(shopDomain, variantId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a variant</summary>
    [HttpDelete("{variantId}")]
    public async Task<IActionResult> DeleteVariantAsync([FromQuery] string shopDomain, string productId, string variantId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _variantService.DeleteVariantAsync(shopDomain, productId, variantId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

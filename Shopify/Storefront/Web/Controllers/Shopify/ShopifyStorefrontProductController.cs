using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/storefront/products")]
[Produces("application/json")]
public class ShopifyStorefrontProductController : ControllerBase
{
    private readonly IShopifyStorefrontProductService _productService;
    public ShopifyStorefrontProductController(IShopifyStorefrontProductService productService) { _productService = productService; }

    /// <summary>Get products from storefront</summary>
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync([FromQuery] string shopDomain, [FromQuery] int first = 10, [FromQuery] string? after = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetProductsAsync(shopDomain, first, after);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get product by handle</summary>
    [HttpGet("handle/{handle}")]
    public async Task<IActionResult> GetProductByHandleAsync([FromQuery] string shopDomain, string handle)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetProductByHandleAsync(shopDomain, handle);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get product by ID</summary>
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductByIdAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetProductByIdAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get collections</summary>
    [HttpGet("/api/shopify/storefront/collections")]
    public async Task<IActionResult> GetCollectionsAsync([FromQuery] string shopDomain, [FromQuery] int first = 10)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetCollectionsAsync(shopDomain, first);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get collection by handle</summary>
    [HttpGet("/api/shopify/storefront/collections/{handle}")]
    public async Task<IActionResult> GetCollectionByHandleAsync([FromQuery] string shopDomain, string handle)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetCollectionByHandleAsync(shopDomain, handle);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

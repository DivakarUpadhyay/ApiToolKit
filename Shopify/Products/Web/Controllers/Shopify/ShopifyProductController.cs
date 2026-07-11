using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Products API</summary>
[ApiController]
[Route("api/shopify/products")]
[Produces("application/json")]
public class ShopifyProductController : ControllerBase
{
    private readonly IShopifyProductService _productService;

    public ShopifyProductController(IShopifyProductService productService)
    {
        _productService = productService;
    }

    /// <summary>List all products</summary>
    [HttpGet]
    public async Task<IActionResult> ListProductsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null, [FromQuery] string? status = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.ListProductsAsync(shopDomain, limit, status);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a product by ID</summary>
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.GetProductAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count products</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountProductsAsync([FromQuery] string shopDomain, [FromQuery] string? status = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.CountProductsAsync(shopDomain, status);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a product</summary>
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromQuery] string shopDomain, [FromBody] CreateProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.CreateProductAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a product</summary>
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductAsync([FromQuery] string shopDomain, string productId, [FromBody] UpdateProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.UpdateProductAsync(shopDomain, productId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a product</summary>
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProductAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _productService.DeleteProductAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Product Images API</summary>
[ApiController]
[Route("api/shopify/products/{productId}/images")]
[Produces("application/json")]
public class ShopifyImageController : ControllerBase
{
    private readonly IShopifyImageService _imageService;

    public ShopifyImageController(IShopifyImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>List all images for a product</summary>
    [HttpGet]
    public async Task<IActionResult> ListImagesAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.ListImagesAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get an image by ID</summary>
    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImageAsync([FromQuery] string shopDomain, string productId, string imageId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.GetImageAsync(shopDomain, productId, imageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count images for a product</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountImagesAsync([FromQuery] string shopDomain, string productId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.CountImagesAsync(shopDomain, productId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create an image for a product</summary>
    [HttpPost]
    public async Task<IActionResult> CreateImageAsync([FromQuery] string shopDomain, string productId, [FromBody] CreateImageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.CreateImageAsync(shopDomain, productId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an image</summary>
    [HttpPut("{imageId}")]
    public async Task<IActionResult> UpdateImageAsync([FromQuery] string shopDomain, string productId, string imageId, [FromBody] UpdateImageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.UpdateImageAsync(shopDomain, productId, imageId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an image</summary>
    [HttpDelete("{imageId}")]
    public async Task<IActionResult> DeleteImageAsync([FromQuery] string shopDomain, string productId, string imageId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _imageService.DeleteImageAsync(shopDomain, productId, imageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Pages API</summary>
[ApiController]
[Route("api/shopify/pages")]
[Produces("application/json")]
public class ShopifyPageController : ControllerBase
{
    private readonly IShopifyPageService _pageService;

    public ShopifyPageController(IShopifyPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>List all pages</summary>
    [HttpGet]
    public async Task<IActionResult> ListPagesAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null, [FromQuery] string? sinceId = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.ListPagesAsync(shopDomain, limit, sinceId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a page by ID</summary>
    [HttpGet("{pageId}")]
    public async Task<IActionResult> GetPageAsync([FromQuery] string shopDomain, string pageId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.GetPageAsync(shopDomain, pageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count pages</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountPagesAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.CountPagesAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a page</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePageAsync([FromQuery] string shopDomain, [FromBody] CreatePageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.CreatePageAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a page</summary>
    [HttpPut("{pageId}")]
    public async Task<IActionResult> UpdatePageAsync([FromQuery] string shopDomain, string pageId, [FromBody] UpdatePageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.UpdatePageAsync(shopDomain, pageId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a page</summary>
    [HttpDelete("{pageId}")]
    public async Task<IActionResult> DeletePageAsync([FromQuery] string shopDomain, string pageId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _pageService.DeletePageAsync(shopDomain, pageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

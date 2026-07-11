using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Blogs API</summary>
[ApiController]
[Route("api/shopify/blogs")]
[Produces("application/json")]
public class ShopifyBlogController : ControllerBase
{
    private readonly IShopifyBlogService _blogService;

    public ShopifyBlogController(IShopifyBlogService blogService)
    {
        _blogService = blogService;
    }

    /// <summary>List all blogs</summary>
    [HttpGet]
    public async Task<IActionResult> ListBlogsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.ListBlogsAsync(shopDomain, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a blog by ID</summary>
    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlogAsync([FromQuery] string shopDomain, string blogId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.GetBlogAsync(shopDomain, blogId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count blogs</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountBlogsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.CountBlogsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a blog</summary>
    [HttpPost]
    public async Task<IActionResult> CreateBlogAsync([FromQuery] string shopDomain, [FromBody] CreateBlogRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.CreateBlogAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a blog</summary>
    [HttpPut("{blogId}")]
    public async Task<IActionResult> UpdateBlogAsync([FromQuery] string shopDomain, string blogId, [FromBody] UpdateBlogRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.UpdateBlogAsync(shopDomain, blogId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a blog</summary>
    [HttpDelete("{blogId}")]
    public async Task<IActionResult> DeleteBlogAsync([FromQuery] string shopDomain, string blogId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _blogService.DeleteBlogAsync(shopDomain, blogId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

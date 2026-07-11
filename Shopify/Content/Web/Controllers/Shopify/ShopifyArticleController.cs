using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Articles API</summary>
[ApiController]
[Route("api/shopify/blogs/{blogId}/articles")]
[Produces("application/json")]
public class ShopifyArticleController : ControllerBase
{
    private readonly IShopifyArticleService _articleService;

    public ShopifyArticleController(IShopifyArticleService articleService)
    {
        _articleService = articleService;
    }

    /// <summary>List all articles in a blog</summary>
    [HttpGet]
    public async Task<IActionResult> ListArticlesAsync([FromQuery] string shopDomain, string blogId, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.ListArticlesAsync(shopDomain, blogId, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get an article by ID</summary>
    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetArticleAsync([FromQuery] string shopDomain, string blogId, string articleId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.GetArticleAsync(shopDomain, blogId, articleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count articles in a blog</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountArticlesAsync([FromQuery] string shopDomain, string blogId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.CountArticlesAsync(shopDomain, blogId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create an article in a blog</summary>
    [HttpPost]
    public async Task<IActionResult> CreateArticleAsync([FromQuery] string shopDomain, string blogId, [FromBody] CreateArticleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.CreateArticleAsync(shopDomain, blogId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an article</summary>
    [HttpPut("{articleId}")]
    public async Task<IActionResult> UpdateArticleAsync([FromQuery] string shopDomain, string blogId, string articleId, [FromBody] UpdateArticleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.UpdateArticleAsync(shopDomain, blogId, articleId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an article</summary>
    [HttpDelete("{articleId}")]
    public async Task<IActionResult> DeleteArticleAsync([FromQuery] string shopDomain, string blogId, string articleId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.DeleteArticleAsync(shopDomain, blogId, articleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get all article authors</summary>
    [HttpGet("/api/shopify/articles/authors")]
    public async Task<IActionResult> GetArticleAuthorsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.GetArticleAuthorsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get all article tags</summary>
    [HttpGet("/api/shopify/articles/tags")]
    public async Task<IActionResult> GetArticleTagsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _articleService.GetArticleTagsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

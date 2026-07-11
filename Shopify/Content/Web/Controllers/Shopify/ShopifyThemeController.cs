using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Themes API</summary>
[ApiController]
[Route("api/shopify/themes")]
[Produces("application/json")]
public class ShopifyThemeController : ControllerBase
{
    private readonly IShopifyThemeService _themeService;

    public ShopifyThemeController(IShopifyThemeService themeService)
    {
        _themeService = themeService;
    }

    /// <summary>List all themes</summary>
    [HttpGet]
    public async Task<IActionResult> ListThemesAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _themeService.ListThemesAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a theme by ID</summary>
    [HttpGet("{themeId}")]
    public async Task<IActionResult> GetThemeAsync([FromQuery] string shopDomain, string themeId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _themeService.GetThemeAsync(shopDomain, themeId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a theme</summary>
    [HttpPost]
    public async Task<IActionResult> CreateThemeAsync([FromQuery] string shopDomain, [FromBody] CreateThemeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _themeService.CreateThemeAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a theme</summary>
    [HttpPut("{themeId}")]
    public async Task<IActionResult> UpdateThemeAsync([FromQuery] string shopDomain, string themeId, [FromBody] UpdateThemeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _themeService.UpdateThemeAsync(shopDomain, themeId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a theme</summary>
    [HttpDelete("{themeId}")]
    public async Task<IActionResult> DeleteThemeAsync([FromQuery] string shopDomain, string themeId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _themeService.DeleteThemeAsync(shopDomain, themeId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Price Rules API</summary>
[ApiController]
[Route("api/shopify/price-rules")]
[Produces("application/json")]
public class ShopifyPriceRuleController : ControllerBase
{
    private readonly IShopifyPriceRuleService _priceRuleService;

    public ShopifyPriceRuleController(IShopifyPriceRuleService priceRuleService)
    {
        _priceRuleService = priceRuleService;
    }

    /// <summary>List all price rules</summary>
    [HttpGet]
    public async Task<IActionResult> ListPriceRulesAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.ListPriceRulesAsync(shopDomain, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a price rule by ID</summary>
    [HttpGet("{priceRuleId}")]
    public async Task<IActionResult> GetPriceRuleAsync([FromQuery] string shopDomain, string priceRuleId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.GetPriceRuleAsync(shopDomain, priceRuleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count price rules</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountPriceRulesAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.CountPriceRulesAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a price rule</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePriceRuleAsync([FromQuery] string shopDomain, [FromBody] CreatePriceRuleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.CreatePriceRuleAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a price rule</summary>
    [HttpPut("{priceRuleId}")]
    public async Task<IActionResult> UpdatePriceRuleAsync([FromQuery] string shopDomain, string priceRuleId, [FromBody] UpdatePriceRuleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.UpdatePriceRuleAsync(shopDomain, priceRuleId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a price rule</summary>
    [HttpDelete("{priceRuleId}")]
    public async Task<IActionResult> DeletePriceRuleAsync([FromQuery] string shopDomain, string priceRuleId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _priceRuleService.DeletePriceRuleAsync(shopDomain, priceRuleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

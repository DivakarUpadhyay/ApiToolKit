using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Discount Codes API</summary>
[ApiController]
[Route("api/shopify/price-rules/{priceRuleId}/discount-codes")]
[Produces("application/json")]
public class ShopifyDiscountCodeController : ControllerBase
{
    private readonly IShopifyDiscountCodeService _discountCodeService;

    public ShopifyDiscountCodeController(IShopifyDiscountCodeService discountCodeService)
    {
        _discountCodeService = discountCodeService;
    }

    /// <summary>List all discount codes for a price rule</summary>
    [HttpGet]
    public async Task<IActionResult> ListDiscountCodesAsync([FromQuery] string shopDomain, string priceRuleId, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.ListDiscountCodesAsync(shopDomain, priceRuleId, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a discount code by ID</summary>
    [HttpGet("{discountCodeId}")]
    public async Task<IActionResult> GetDiscountCodeAsync([FromQuery] string shopDomain, string priceRuleId, string discountCodeId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.GetDiscountCodeAsync(shopDomain, priceRuleId, discountCodeId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count discount codes for a price rule</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountDiscountCodesAsync([FromQuery] string shopDomain, string priceRuleId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.CountDiscountCodesAsync(shopDomain, priceRuleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a discount code for a price rule</summary>
    [HttpPost]
    public async Task<IActionResult> CreateDiscountCodeAsync([FromQuery] string shopDomain, string priceRuleId, [FromBody] CreateDiscountCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.CreateDiscountCodeAsync(shopDomain, priceRuleId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a discount code</summary>
    [HttpPut("{discountCodeId}")]
    public async Task<IActionResult> UpdateDiscountCodeAsync([FromQuery] string shopDomain, string priceRuleId, string discountCodeId, [FromBody] UpdateDiscountCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.UpdateDiscountCodeAsync(shopDomain, priceRuleId, discountCodeId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a discount code</summary>
    [HttpDelete("{discountCodeId}")]
    public async Task<IActionResult> DeleteDiscountCodeAsync([FromQuery] string shopDomain, string priceRuleId, string discountCodeId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.DeleteDiscountCodeAsync(shopDomain, priceRuleId, discountCodeId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch create discount codes for a price rule</summary>
    [HttpPost("batch")]
    public async Task<IActionResult> CreateBatchDiscountCodesAsync([FromQuery] string shopDomain, string priceRuleId, [FromBody] BatchDiscountCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.CreateBatchDiscountCodesAsync(shopDomain, priceRuleId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get batch discount code creation job status</summary>
    [HttpGet("batch/{batchId}")]
    public async Task<IActionResult> GetBatchDiscountCodeStatusAsync([FromQuery] string shopDomain, string priceRuleId, string batchId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _discountCodeService.GetBatchDiscountCodeStatusAsync(shopDomain, priceRuleId, batchId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

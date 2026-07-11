using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Draft Orders API</summary>
[ApiController]
[Route("api/shopify/draft-orders")]
[Produces("application/json")]
public class ShopifyDraftOrderController : ControllerBase
{
    private readonly IShopifyDraftOrderService _draftOrderService;

    public ShopifyDraftOrderController(IShopifyDraftOrderService draftOrderService)
    {
        _draftOrderService = draftOrderService;
    }

    /// <summary>List all draft orders</summary>
    [HttpGet]
    public async Task<IActionResult> ListDraftOrdersAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.ListDraftOrdersAsync(shopDomain, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a draft order by ID</summary>
    [HttpGet("{draftOrderId}")]
    public async Task<IActionResult> GetDraftOrderAsync([FromQuery] string shopDomain, string draftOrderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.GetDraftOrderAsync(shopDomain, draftOrderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count draft orders</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountDraftOrdersAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.CountDraftOrdersAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a draft order</summary>
    [HttpPost]
    public async Task<IActionResult> CreateDraftOrderAsync([FromQuery] string shopDomain, [FromBody] CreateDraftOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.CreateDraftOrderAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a draft order</summary>
    [HttpPut("{draftOrderId}")]
    public async Task<IActionResult> UpdateDraftOrderAsync([FromQuery] string shopDomain, string draftOrderId, [FromBody] UpdateDraftOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.UpdateDraftOrderAsync(shopDomain, draftOrderId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Complete a draft order</summary>
    [HttpPost("{draftOrderId}/complete")]
    public async Task<IActionResult> CompleteDraftOrderAsync([FromQuery] string shopDomain, string draftOrderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.CompleteDraftOrderAsync(shopDomain, draftOrderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a draft order</summary>
    [HttpDelete("{draftOrderId}")]
    public async Task<IActionResult> DeleteDraftOrderAsync([FromQuery] string shopDomain, string draftOrderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _draftOrderService.DeleteDraftOrderAsync(shopDomain, draftOrderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

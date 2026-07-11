using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>
/// Manages Shopify Fulfillment Orders
/// </summary>
[ApiController]
[Route("api/shopify/fulfillment-orders")]
[Produces("application/json")]
public class ShopifyFulfillmentOrderController : ControllerBase
{
    private readonly IShopifyFulfillmentOrderService _fulfillmentOrderService;

    public ShopifyFulfillmentOrderController(IShopifyFulfillmentOrderService fulfillmentOrderService)
    {
        _fulfillmentOrderService = fulfillmentOrderService;
    }

    /// <summary>
    /// List fulfillment orders for an order
    /// </summary>
    [HttpGet("orders/{orderId}")]
    public async Task<IActionResult> ListFulfillmentOrders(string orderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentOrderService.ListFulfillmentOrdersAsync(shopDomain, orderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Get a specific fulfillment order
    /// </summary>
    [HttpGet("{fulfillmentOrderId}")]
    public async Task<IActionResult> GetFulfillmentOrder(string fulfillmentOrderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentOrderService.GetFulfillmentOrderAsync(shopDomain, fulfillmentOrderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Cancel a fulfillment order
    /// </summary>
    [HttpPost("{fulfillmentOrderId}/cancel")]
    public async Task<IActionResult> CancelFulfillmentOrder(string fulfillmentOrderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentOrderService.CancelFulfillmentOrderAsync(shopDomain, fulfillmentOrderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Move a fulfillment order to a new location
    /// </summary>
    [HttpPost("{fulfillmentOrderId}/move")]
    public async Task<IActionResult> MoveFulfillmentOrder(string fulfillmentOrderId, [FromQuery] string shopDomain, [FromQuery] string newLocationId)
    {
        var result = await _fulfillmentOrderService.MoveFulfillmentOrderAsync(shopDomain, fulfillmentOrderId, newLocationId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Release holds on a fulfillment order
    /// </summary>
    [HttpPost("{fulfillmentOrderId}/release-holds")]
    public async Task<IActionResult> ReleaseHolds(string fulfillmentOrderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentOrderService.ReleaseHoldsAsync(shopDomain, fulfillmentOrderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }
}

using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>
/// Manages Shopify Fulfillments
/// </summary>
[ApiController]
[Route("api/shopify/fulfillments")]
[Produces("application/json")]
public class ShopifyFulfillmentController : ControllerBase
{
    private readonly IShopifyFulfillmentService _fulfillmentService;

    public ShopifyFulfillmentController(IShopifyFulfillmentService fulfillmentService)
    {
        _fulfillmentService = fulfillmentService;
    }

    /// <summary>
    /// List fulfillments by order ID
    /// </summary>
    [HttpGet("orders/{orderId}")]
    public async Task<IActionResult> ListFulfillmentsByOrder(string orderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentService.ListFulfillmentsByOrderAsync(shopDomain, orderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Get a specific fulfillment by order ID and fulfillment ID
    /// </summary>
    [HttpGet("orders/{orderId}/{fulfillmentId}")]
    public async Task<IActionResult> GetFulfillment(string orderId, string fulfillmentId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentService.GetFulfillmentAsync(shopDomain, orderId, fulfillmentId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Count fulfillments for an order
    /// </summary>
    [HttpGet("orders/{orderId}/count")]
    public async Task<IActionResult> CountFulfillments(string orderId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentService.CountFulfillmentsAsync(shopDomain, orderId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(new { count = result.Data });
    }

    /// <summary>
    /// Create a new fulfillment
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateFulfillment([FromQuery] string shopDomain, [FromBody] CreateFulfillmentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _fulfillmentService.CreateFulfillmentAsync(shopDomain, request);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return StatusCode(201, result.Data);
    }

    /// <summary>
    /// Cancel a fulfillment
    /// </summary>
    [HttpPost("{fulfillmentId}/cancel")]
    public async Task<IActionResult> CancelFulfillment(string fulfillmentId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentService.CancelFulfillmentAsync(shopDomain, fulfillmentId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Update tracking information for a fulfillment
    /// </summary>
    [HttpPost("{fulfillmentId}/tracking")]
    public async Task<IActionResult> UpdateTracking(string fulfillmentId, [FromQuery] string shopDomain, [FromBody] UpdateTrackingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _fulfillmentService.UpdateTrackingAsync(shopDomain, fulfillmentId, request);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }
}

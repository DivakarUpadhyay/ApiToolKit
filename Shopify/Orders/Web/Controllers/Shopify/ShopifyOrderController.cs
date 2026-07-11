using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Orders API</summary>
[ApiController]
[Route("api/shopify/orders")]
[Produces("application/json")]
public class ShopifyOrderController : ControllerBase
{
    private readonly IShopifyOrderService _orderService;

    public ShopifyOrderController(IShopifyOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>List all orders</summary>
    [HttpGet]
    public async Task<IActionResult> ListOrdersAsync([FromQuery] string shopDomain, [FromQuery] string? status = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.ListOrdersAsync(shopDomain, status, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get an order by ID</summary>
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.GetOrderAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count orders</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountOrdersAsync([FromQuery] string shopDomain, [FromQuery] string? status = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.CountOrdersAsync(shopDomain, status);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create an order</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromQuery] string shopDomain, [FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.CreateOrderAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an order</summary>
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrderAsync([FromQuery] string shopDomain, string orderId, [FromBody] UpdateOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.UpdateOrderAsync(shopDomain, orderId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Cancel an order</summary>
    [HttpPost("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrderAsync([FromQuery] string shopDomain, string orderId, [FromQuery] string? reason = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.CancelOrderAsync(shopDomain, orderId, reason);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Close an order</summary>
    [HttpPost("{orderId}/close")]
    public async Task<IActionResult> CloseOrderAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.CloseOrderAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reopen an order</summary>
    [HttpPost("{orderId}/open")]
    public async Task<IActionResult> ReopenOrderAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.ReopenOrderAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an order</summary>
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrderAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _orderService.DeleteOrderAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

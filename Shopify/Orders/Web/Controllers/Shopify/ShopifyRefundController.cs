using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Refunds API</summary>
[ApiController]
[Route("api/shopify/orders/{orderId}/refunds")]
[Produces("application/json")]
public class ShopifyRefundController : ControllerBase
{
    private readonly IShopifyRefundService _refundService;

    public ShopifyRefundController(IShopifyRefundService refundService)
    {
        _refundService = refundService;
    }

    /// <summary>List refunds for an order</summary>
    [HttpGet]
    public async Task<IActionResult> ListRefundsAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _refundService.ListRefundsAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a refund by ID</summary>
    [HttpGet("{refundId}")]
    public async Task<IActionResult> GetRefundAsync([FromQuery] string shopDomain, string orderId, string refundId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _refundService.GetRefundAsync(shopDomain, orderId, refundId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a refund for an order</summary>
    [HttpPost]
    public async Task<IActionResult> CreateRefundAsync([FromQuery] string shopDomain, string orderId, [FromBody] CreateRefundRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _refundService.CreateRefundAsync(shopDomain, orderId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Calculate a refund for an order</summary>
    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateRefundAsync([FromQuery] string shopDomain, string orderId, [FromBody] CalculateRefundRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _refundService.CalculateRefundAsync(shopDomain, orderId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

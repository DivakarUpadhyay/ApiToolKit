using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Payments API (Payouts, Disputes, Balance Transactions)</summary>
[ApiController]
[Route("api/shopify/payments")]
[Produces("application/json")]
public class ShopifyPaymentsController : ControllerBase
{
    private readonly IShopifyPaymentsService _paymentsService;

    public ShopifyPaymentsController(IShopifyPaymentsService paymentsService)
    {
        _paymentsService = paymentsService;
    }

    /// <summary>List all payouts</summary>
    [HttpGet("payouts")]
    public async Task<IActionResult> ListPayoutsAsync([FromQuery] string shopDomain, [FromQuery] string? status = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _paymentsService.ListPayoutsAsync(shopDomain, status, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a payout by ID</summary>
    [HttpGet("payouts/{payoutId}")]
    public async Task<IActionResult> GetPayoutAsync([FromQuery] string shopDomain, string payoutId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _paymentsService.GetPayoutAsync(shopDomain, payoutId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all disputes</summary>
    [HttpGet("disputes")]
    public async Task<IActionResult> ListDisputesAsync([FromQuery] string shopDomain, [FromQuery] string? initiatedAt = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _paymentsService.ListDisputesAsync(shopDomain, initiatedAt, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a dispute by ID</summary>
    [HttpGet("disputes/{disputeId}")]
    public async Task<IActionResult> GetDisputeAsync([FromQuery] string shopDomain, string disputeId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _paymentsService.GetDisputeAsync(shopDomain, disputeId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List balance transactions</summary>
    [HttpGet("balance/transactions")]
    public async Task<IActionResult> ListBalanceTransactionsAsync([FromQuery] string shopDomain, [FromQuery] string? payoutId = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _paymentsService.ListBalanceTransactionsAsync(shopDomain, payoutId, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

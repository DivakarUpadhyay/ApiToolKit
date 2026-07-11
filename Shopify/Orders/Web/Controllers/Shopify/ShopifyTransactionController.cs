using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Transactions API</summary>
[ApiController]
[Route("api/shopify/orders/{orderId}/transactions")]
[Produces("application/json")]
public class ShopifyTransactionController : ControllerBase
{
    private readonly IShopifyTransactionService _transactionService;

    public ShopifyTransactionController(IShopifyTransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    /// <summary>List transactions for an order</summary>
    [HttpGet]
    public async Task<IActionResult> ListTransactionsAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _transactionService.ListTransactionsAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a transaction by ID</summary>
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionAsync([FromQuery] string shopDomain, string orderId, string transactionId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _transactionService.GetTransactionAsync(shopDomain, orderId, transactionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count transactions for an order</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountTransactionsAsync([FromQuery] string shopDomain, string orderId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _transactionService.CountTransactionsAsync(shopDomain, orderId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a transaction for an order</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromQuery] string shopDomain, string orderId, [FromBody] CreateTransactionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _transactionService.CreateTransactionAsync(shopDomain, orderId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

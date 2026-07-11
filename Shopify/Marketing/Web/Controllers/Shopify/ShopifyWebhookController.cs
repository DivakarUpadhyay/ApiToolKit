using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Webhooks API</summary>
[ApiController]
[Route("api/shopify/webhooks")]
[Produces("application/json")]
public class ShopifyWebhookController : ControllerBase
{
    private readonly IShopifyWebhookService _webhookService;

    public ShopifyWebhookController(IShopifyWebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    /// <summary>List all webhooks</summary>
    [HttpGet]
    public async Task<IActionResult> ListWebhooksAsync([FromQuery] string shopDomain, [FromQuery] string? topic = null, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.ListWebhooksAsync(shopDomain, topic, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a webhook by ID</summary>
    [HttpGet("{webhookId}")]
    public async Task<IActionResult> GetWebhookAsync([FromQuery] string shopDomain, string webhookId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.GetWebhookAsync(shopDomain, webhookId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count webhooks</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountWebhooksAsync([FromQuery] string shopDomain, [FromQuery] string? topic = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.CountWebhooksAsync(shopDomain, topic);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a webhook</summary>
    [HttpPost]
    public async Task<IActionResult> CreateWebhookAsync([FromQuery] string shopDomain, [FromBody] CreateWebhookRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.CreateWebhookAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a webhook</summary>
    [HttpPut("{webhookId}")]
    public async Task<IActionResult> UpdateWebhookAsync([FromQuery] string shopDomain, string webhookId, [FromBody] UpdateWebhookRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.UpdateWebhookAsync(shopDomain, webhookId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a webhook</summary>
    [HttpDelete("{webhookId}")]
    public async Task<IActionResult> DeleteWebhookAsync([FromQuery] string shopDomain, string webhookId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _webhookService.DeleteWebhookAsync(shopDomain, webhookId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

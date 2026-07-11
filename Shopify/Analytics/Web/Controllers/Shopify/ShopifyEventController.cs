using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Events API</summary>
[ApiController]
[Route("api/shopify/events")]
[Produces("application/json")]
public class ShopifyEventController : ControllerBase
{
    private readonly IShopifyEventService _eventService;

    public ShopifyEventController(IShopifyEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>List all events</summary>
    [HttpGet]
    public async Task<IActionResult> ListEventsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null, [FromQuery] string? sinceId = null, [FromQuery] string? filter = null, [FromQuery] string? verb = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _eventService.ListEventsAsync(shopDomain, limit, sinceId, filter, verb);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get an event by ID</summary>
    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetEventAsync([FromQuery] string shopDomain, string eventId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _eventService.GetEventAsync(shopDomain, eventId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count events</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountEventsAsync([FromQuery] string shopDomain, [FromQuery] string? filter = null, [FromQuery] string? verb = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _eventService.CountEventsAsync(shopDomain, filter, verb);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

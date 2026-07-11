using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Locations API</summary>
[ApiController]
[Route("api/shopify/locations")]
[Produces("application/json")]
public class ShopifyLocationController : ControllerBase
{
    private readonly IShopifyLocationService _locationService;

    public ShopifyLocationController(IShopifyLocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>List all locations</summary>
    [HttpGet]
    public async Task<IActionResult> ListLocationsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _locationService.ListLocationsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count all locations</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountLocationsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _locationService.CountLocationsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a location by ID</summary>
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetLocationAsync([FromQuery] string shopDomain, string locationId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _locationService.GetLocationAsync(shopDomain, locationId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get inventory levels at a location</summary>
    [HttpGet("{locationId}/inventory-levels")]
    public async Task<IActionResult> GetLocationInventoryLevelsAsync([FromQuery] string shopDomain, string locationId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _locationService.GetLocationInventoryLevelsAsync(shopDomain, locationId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

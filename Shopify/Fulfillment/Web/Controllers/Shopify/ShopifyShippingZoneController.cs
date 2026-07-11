using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>
/// Manages Shopify Shipping Zones
/// </summary>
[ApiController]
[Route("api/shopify/shipping-zones")]
[Produces("application/json")]
public class ShopifyShippingZoneController : ControllerBase
{
    private readonly IShopifyShippingZoneService _shippingZoneService;

    public ShopifyShippingZoneController(IShopifyShippingZoneService shippingZoneService)
    {
        _shippingZoneService = shippingZoneService;
    }

    /// <summary>
    /// List all shipping zones
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ListShippingZones([FromQuery] string shopDomain)
    {
        var result = await _shippingZoneService.ListShippingZonesAsync(shopDomain);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }
}

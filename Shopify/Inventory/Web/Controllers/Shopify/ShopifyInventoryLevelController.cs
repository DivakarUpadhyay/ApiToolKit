using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Inventory Levels API</summary>
[ApiController]
[Route("api/shopify/inventory/levels")]
[Produces("application/json")]
public class ShopifyInventoryLevelController : ControllerBase
{
    private readonly IShopifyInventoryLevelService _inventoryLevelService;

    public ShopifyInventoryLevelController(IShopifyInventoryLevelService inventoryLevelService)
    {
        _inventoryLevelService = inventoryLevelService;
    }

    /// <summary>List inventory levels by inventory item IDs and/or location IDs</summary>
    [HttpGet]
    public async Task<IActionResult> ListInventoryLevelsAsync([FromQuery] string shopDomain, [FromQuery] string? inventoryItemIds = null, [FromQuery] string? locationIds = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryLevelService.ListInventoryLevelsAsync(shopDomain, inventoryItemIds, locationIds);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Adjust inventory level quantity</summary>
    [HttpPost("adjust")]
    public async Task<IActionResult> AdjustInventoryLevelAsync([FromQuery] string shopDomain, [FromBody] AdjustInventoryLevelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryLevelService.AdjustInventoryLevelAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Connect an inventory item to a location</summary>
    [HttpPost("connect")]
    public async Task<IActionResult> ConnectInventoryLevelAsync([FromQuery] string shopDomain, [FromBody] ConnectInventoryLevelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryLevelService.ConnectInventoryLevelAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set absolute inventory level quantity</summary>
    [HttpPost("set")]
    public async Task<IActionResult> SetInventoryLevelAsync([FromQuery] string shopDomain, [FromBody] SetInventoryLevelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryLevelService.SetInventoryLevelAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Disconnect an inventory item from a location</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteInventoryLevelAsync([FromQuery] string shopDomain, [FromQuery] string inventoryItemId, [FromQuery] string locationId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        if (string.IsNullOrWhiteSpace(inventoryItemId)) return BadRequest("inventoryItemId is required.");
        if (string.IsNullOrWhiteSpace(locationId)) return BadRequest("locationId is required.");
        var result = await _inventoryLevelService.DeleteInventoryLevelAsync(shopDomain, inventoryItemId, locationId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

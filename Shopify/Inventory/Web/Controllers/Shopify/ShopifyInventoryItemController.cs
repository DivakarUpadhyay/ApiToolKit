using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Inventory Items API</summary>
[ApiController]
[Route("api/shopify/inventory/items")]
[Produces("application/json")]
public class ShopifyInventoryItemController : ControllerBase
{
    private readonly IShopifyInventoryItemService _inventoryItemService;

    public ShopifyInventoryItemController(IShopifyInventoryItemService inventoryItemService)
    {
        _inventoryItemService = inventoryItemService;
    }

    /// <summary>List inventory items by IDs</summary>
    [HttpGet]
    public async Task<IActionResult> ListInventoryItemsAsync([FromQuery] string shopDomain, [FromQuery] string ids, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        if (string.IsNullOrWhiteSpace(ids)) return BadRequest("ids is required.");
        var result = await _inventoryItemService.ListInventoryItemsAsync(shopDomain, ids, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a single inventory item by ID</summary>
    [HttpGet("{inventoryItemId}")]
    public async Task<IActionResult> GetInventoryItemAsync([FromQuery] string shopDomain, string inventoryItemId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryItemService.GetInventoryItemAsync(shopDomain, inventoryItemId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an inventory item (cost, tracked)</summary>
    [HttpPut("{inventoryItemId}")]
    public async Task<IActionResult> UpdateInventoryItemAsync([FromQuery] string shopDomain, string inventoryItemId, [FromBody] UpdateInventoryItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _inventoryItemService.UpdateInventoryItemAsync(shopDomain, inventoryItemId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Merchant.InventoriesProductLocalInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// products / localInventories (v1)
/// </summary>
[ApiController]
[Route("api/merchant/inventories")]
[Produces("application/json")]
public class MerchantInventoriesProductLocalInventoryController : ControllerBase
{
    private readonly IMerchantInventoriesProductLocalInventoryService _inventoriesProductLocalInventoryService;

    public MerchantInventoriesProductLocalInventoryController(IMerchantInventoriesProductLocalInventoryService inventoriesProductLocalInventoryService)
    {
        _inventoriesProductLocalInventoryService = inventoriesProductLocalInventoryService;
    }

    /// <summary>Lists the `LocalInventory` resources for the given product in your merchant account.</summary>
    [HttpGet("accounts/{accountId}/products/{productId}/localInventories")]
    public async Task<IActionResult> ListLocalInventoriesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLocalInventoriesRequest { AccountKey = accountKey, AccountId = accountId, ProductId = productId, PageToken = pageToken, PageSize = pageSize };
        var result = await _inventoriesProductLocalInventoryService.ListLocalInventoriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a `LocalInventory` resource to a product in your merchant account.</summary>
    [HttpPost("accounts/{accountId}/products/{productId}/localInventories/insert")]
    public async Task<IActionResult> InsertLocalInventoryAsync([FromRoute] string accountId, [FromRoute] string productId, [FromBody] InsertLocalInventoryRequest request)
    {
        request.AccountId = accountId;
        request.ProductId = productId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventoriesProductLocalInventoryService.InsertLocalInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes the specified `LocalInventory` from the given product in your merchant account.</summary>
    [HttpDelete("accounts/{accountId}/products/{productId}/localInventories/{localInventoryId}")]
    public async Task<IActionResult> DeleteLocalInventoryAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productId, [FromRoute] string localInventoryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteLocalInventoryRequest { AccountKey = accountKey, AccountId = accountId, ProductId = productId, LocalInventoryId = localInventoryId };
        var result = await _inventoriesProductLocalInventoryService.DeleteLocalInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

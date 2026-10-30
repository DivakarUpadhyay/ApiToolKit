using ApiToolKit.Application.DTOs.Merchant.InventoriesProductRegionalInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// products / regionalInventories (v1)
/// </summary>
[ApiController]
[Route("api/merchant/inventories")]
[Produces("application/json")]
public class MerchantInventoriesProductRegionalInventoryController : ControllerBase
{
    private readonly IMerchantInventoriesProductRegionalInventoryService _inventoriesProductRegionalInventoryService;

    public MerchantInventoriesProductRegionalInventoryController(IMerchantInventoriesProductRegionalInventoryService inventoriesProductRegionalInventoryService)
    {
        _inventoriesProductRegionalInventoryService = inventoriesProductRegionalInventoryService;
    }

    /// <summary>Deletes the specified `RegionalInventory` resource from the given product in your merchant account.</summary>
    [HttpDelete("accounts/{accountId}/products/{productId}/regionalInventories/{regionalInventoryId}")]
    public async Task<IActionResult> DeleteRegionalInventoryAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productId, [FromRoute] string regionalInventoryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteRegionalInventoryRequest { AccountKey = accountKey, AccountId = accountId, ProductId = productId, RegionalInventoryId = regionalInventoryId };
        var result = await _inventoriesProductRegionalInventoryService.DeleteRegionalInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the `RegionalInventory` resources for the given product in your merchant account.</summary>
    [HttpGet("accounts/{accountId}/products/{productId}/regionalInventories")]
    public async Task<IActionResult> ListRegionalInventoriesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListRegionalInventoriesRequest { AccountKey = accountKey, AccountId = accountId, ProductId = productId, PageToken = pageToken, PageSize = pageSize };
        var result = await _inventoriesProductRegionalInventoryService.ListRegionalInventoriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a `RegionalInventory` to a given product in your merchant account.</summary>
    [HttpPost("accounts/{accountId}/products/{productId}/regionalInventories/insert")]
    public async Task<IActionResult> InsertRegionalInventoryAsync([FromRoute] string accountId, [FromRoute] string productId, [FromBody] InsertRegionalInventoryRequest request)
    {
        request.AccountId = accountId;
        request.ProductId = productId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventoriesProductRegionalInventoryService.InsertRegionalInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

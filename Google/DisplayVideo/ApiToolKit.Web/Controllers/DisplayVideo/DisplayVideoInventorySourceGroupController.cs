using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// inventorySourceGroups (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoInventorySourceGroupController : ControllerBase
{
    private readonly IDisplayVideoInventorySourceGroupService _inventorySourceGroupService;

    public DisplayVideoInventorySourceGroupController(IDisplayVideoInventorySourceGroupService inventorySourceGroupService)
    {
        _inventorySourceGroupService = inventorySourceGroupService;
    }

    /// <summary>Gets an inventory source group.</summary>
    [HttpGet("inventorySourceGroups/{inventorySourceGroupId}")]
    public async Task<IActionResult> GetInventorySourceGroupAsync([FromQuery] string accountKey, [FromRoute] string inventorySourceGroupId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetInventorySourceGroupRequest { AccountKey = accountKey, InventorySourceGroupId = inventorySourceGroupId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _inventorySourceGroupService.GetInventorySourceGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an inventory source group.</summary>
    [HttpDelete("inventorySourceGroups/{inventorySourceGroupId}")]
    public async Task<IActionResult> DeleteInventorySourceGroupAsync([FromQuery] string accountKey, [FromRoute] string inventorySourceGroupId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteInventorySourceGroupRequest { AccountKey = accountKey, InventorySourceGroupId = inventorySourceGroupId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _inventorySourceGroupService.DeleteInventorySourceGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists inventory source groups that are accessible to the current user.</summary>
    [HttpGet("inventorySourceGroups")]
    public async Task<IActionResult> ListInventorySourceGroupsAsync([FromQuery] string accountKey, [FromQuery] string? advertiserId = null, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListInventorySourceGroupsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, OrderBy = orderBy, PartnerId = partnerId, PageToken = pageToken, PageSize = pageSize };
        var result = await _inventorySourceGroupService.ListInventorySourceGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an inventory source group.</summary>
    [HttpPatch("inventorySourceGroups/{inventorySourceGroupId}")]
    public async Task<IActionResult> UpdateInventorySourceGroupAsync([FromRoute] string inventorySourceGroupId, [FromBody] UpdateInventorySourceGroupRequest request)
    {
        request.InventorySourceGroupId = inventorySourceGroupId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceGroupService.UpdateInventorySourceGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new inventory source group.</summary>
    [HttpPost("inventorySourceGroups")]
    public async Task<IActionResult> CreateInventorySourceGroupAsync([FromBody] CreateInventorySourceGroupRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceGroupService.CreateInventorySourceGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

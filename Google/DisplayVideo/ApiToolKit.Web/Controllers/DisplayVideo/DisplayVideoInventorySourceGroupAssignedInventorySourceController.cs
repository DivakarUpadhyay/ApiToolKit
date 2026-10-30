using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroupAssignedInventorySource;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// inventorySourceGroups / assignedInventorySources (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoInventorySourceGroupAssignedInventorySourceController : ControllerBase
{
    private readonly IDisplayVideoInventorySourceGroupAssignedInventorySourceService _inventorySourceGroupAssignedInventorySourceService;

    public DisplayVideoInventorySourceGroupAssignedInventorySourceController(IDisplayVideoInventorySourceGroupAssignedInventorySourceService inventorySourceGroupAssignedInventorySourceService)
    {
        _inventorySourceGroupAssignedInventorySourceService = inventorySourceGroupAssignedInventorySourceService;
    }

    /// <summary>Lists inventory sources assigned to an inventory source group.</summary>
    [HttpGet("inventorySourceGroups/{inventorySourceGroupId}/assignedInventorySources")]
    public async Task<IActionResult> ListAssignedInventorySourcesAsync([FromQuery] string accountKey, [FromRoute] string inventorySourceGroupId, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedInventorySourcesRequest { AccountKey = accountKey, InventorySourceGroupId = inventorySourceGroupId, OrderBy = orderBy, Filter = filter, PageSize = pageSize, AdvertiserId = advertiserId, PartnerId = partnerId, PageToken = pageToken };
        var result = await _inventorySourceGroupAssignedInventorySourceService.ListAssignedInventorySourcesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bulk edits multiple assignments between inventory sources and a single inventory source group.</summary>
    [HttpPost("inventorySourceGroups/{inventorySourceGroupId}/assignedInventorySources/bulkEdit")]
    public async Task<IActionResult> BulkEditAssignedInventorySourceAsync([FromRoute] string inventorySourceGroupId, [FromBody] BulkEditAssignedInventorySourceRequest request)
    {
        request.InventorySourceGroupId = inventorySourceGroupId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceGroupAssignedInventorySourceService.BulkEditAssignedInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates an assignment between an inventory source and an inventory source group.</summary>
    [HttpPost("inventorySourceGroups/{inventorySourceGroupId}/assignedInventorySources")]
    public async Task<IActionResult> CreateAssignedInventorySourceAsync([FromRoute] string inventorySourceGroupId, [FromBody] CreateAssignedInventorySourceRequest request)
    {
        request.InventorySourceGroupId = inventorySourceGroupId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceGroupAssignedInventorySourceService.CreateAssignedInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes the assignment between an inventory source and an inventory source group.</summary>
    [HttpDelete("inventorySourceGroups/{inventorySourceGroupId}/assignedInventorySources/{assignedInventorySourceId}")]
    public async Task<IActionResult> DeleteAssignedInventorySourceAsync([FromQuery] string accountKey, [FromRoute] string inventorySourceGroupId, [FromRoute] string assignedInventorySourceId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedInventorySourceRequest { AccountKey = accountKey, InventorySourceGroupId = inventorySourceGroupId, AssignedInventorySourceId = assignedInventorySourceId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _inventorySourceGroupAssignedInventorySourceService.DeleteAssignedInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

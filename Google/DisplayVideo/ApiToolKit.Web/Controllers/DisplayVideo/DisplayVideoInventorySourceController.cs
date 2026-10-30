using ApiToolKit.Application.DTOs.DisplayVideo.InventorySource;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// inventorySources (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoInventorySourceController : ControllerBase
{
    private readonly IDisplayVideoInventorySourceService _inventorySourceService;

    public DisplayVideoInventorySourceController(IDisplayVideoInventorySourceService inventorySourceService)
    {
        _inventorySourceService = inventorySourceService;
    }

    /// <summary>Updates an existing inventory source.</summary>
    [HttpPatch("inventorySources/{inventorySourceId}")]
    public async Task<IActionResult> UpdateInventorySourceAsync([FromRoute] string inventorySourceId, [FromBody] UpdateInventorySourceRequest request)
    {
        request.InventorySourceId = inventorySourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceService.UpdateInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an inventory source.</summary>
    [HttpGet("inventorySources/{inventorySourceId}")]
    public async Task<IActionResult> GetInventorySourceAsync([FromQuery] string accountKey, [FromRoute] string inventorySourceId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetInventorySourceRequest { AccountKey = accountKey, InventorySourceId = inventorySourceId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _inventorySourceService.GetInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new inventory source.</summary>
    [HttpPost("inventorySources")]
    public async Task<IActionResult> CreateInventorySourceAsync([FromBody] CreateInventorySourceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceService.CreateInventorySourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Edits read/write accessors of an inventory source.</summary>
    [HttpPost("inventorySources/{inventorySourceId}/editInventorySourceReadWriteAccessors")]
    public async Task<IActionResult> EditInventorySourceReadWriteAccessorsAsync([FromRoute] string inventorySourceId, [FromBody] EditInventorySourceReadWriteAccessorsRequest request)
    {
        request.InventorySourceId = inventorySourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _inventorySourceService.EditInventorySourceReadWriteAccessorsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists inventory sources that are accessible to the current user.</summary>
    [HttpGet("inventorySources")]
    public async Task<IActionResult> ListInventorySourcesAsync([FromQuery] string accountKey, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] string? advertiserId = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListInventorySourcesRequest { AccountKey = accountKey, PageSize = pageSize, Filter = filter, PartnerId = partnerId, PageToken = pageToken, AdvertiserId = advertiserId, OrderBy = orderBy };
        var result = await _inventorySourceService.ListInventorySourcesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

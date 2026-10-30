using ApiToolKit.Application.DTOs.TagManager.Zone;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace zones.
/// </summary>
[ApiController]
[Route("api/tagmanager/zones")]
[Produces("application/json")]
public class TagManagerZoneController : ControllerBase
{
    private readonly ITagManagerZoneService _zoneService;

    public TagManagerZoneController(ITagManagerZoneService zoneService)
    {
        _zoneService = zoneService;
    }

    /// <summary>List zones in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListZonesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListZonesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _zoneService.ListZonesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one zone.</summary>
    [HttpGet("{zoneId}")]
    public async Task<IActionResult> GetZoneAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string zoneId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetZoneRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, ZoneId = zoneId };
        var result = await _zoneService.GetZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a zone.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateZoneAsync([FromBody] CreateZoneRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _zoneService.CreateZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a zone.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateZoneAsync([FromBody] UpdateZoneRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _zoneService.UpdateZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a zone.</summary>
    [HttpDelete("{zoneId}")]
    public async Task<IActionResult> DeleteZoneAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string zoneId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteZoneRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, ZoneId = zoneId };
        var result = await _zoneService.DeleteZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a zone.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertZoneAsync([FromBody] RevertZoneRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _zoneService.RevertZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

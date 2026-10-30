using ApiToolKit.Application.DTOs.TagManager.GtagConfig;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace gtagconfigs.
/// </summary>
[ApiController]
[Route("api/tagmanager/gtag-config")]
[Produces("application/json")]
public class TagManagerGtagConfigController : ControllerBase
{
    private readonly ITagManagerGtagConfigService _gtagConfigService;

    public TagManagerGtagConfigController(ITagManagerGtagConfigService gtagConfigService)
    {
        _gtagConfigService = gtagConfigService;
    }

    /// <summary>List gtagconfigs in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListGtagConfigsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListGtagConfigsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _gtagConfigService.ListGtagConfigsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one gtagconfig.</summary>
    [HttpGet("{gtagConfigId}")]
    public async Task<IActionResult> GetGtagConfigAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string gtagConfigId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetGtagConfigRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, GtagConfigId = gtagConfigId };
        var result = await _gtagConfigService.GetGtagConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a gtagconfig.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateGtagConfigAsync([FromBody] CreateGtagConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _gtagConfigService.CreateGtagConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a gtagconfig.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateGtagConfigAsync([FromBody] UpdateGtagConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _gtagConfigService.UpdateGtagConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a gtagconfig.</summary>
    [HttpDelete("{gtagConfigId}")]
    public async Task<IActionResult> DeleteGtagConfigAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string gtagConfigId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteGtagConfigRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, GtagConfigId = gtagConfigId };
        var result = await _gtagConfigService.DeleteGtagConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

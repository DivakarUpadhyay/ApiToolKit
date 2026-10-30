using ApiToolKit.Application.DTOs.TagManager.Workspace;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspaces and container versions.
/// </summary>
[ApiController]
[Route("api/tagmanager/workspaces")]
[Produces("application/json")]
public class TagManagerWorkspaceController : ControllerBase
{
    private readonly ITagManagerWorkspaceService _workspaceService;

    public TagManagerWorkspaceController(ITagManagerWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    /// <summary>List workspaces in a container.</summary>
    [HttpGet]
    public async Task<IActionResult> ListWorkspacesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new ListWorkspacesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _workspaceService.ListWorkspacesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one workspace.</summary>
    [HttpGet("{workspaceId}")]
    public async Task<IActionResult> GetWorkspaceAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetWorkspaceRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _workspaceService.GetWorkspaceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a workspace.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateWorkspaceAsync([FromBody] CreateWorkspaceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.CreateWorkspaceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a workspace.</summary>
    [HttpDelete("{workspaceId}")]
    public async Task<IActionResult> DeleteWorkspaceAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new DeleteWorkspaceRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _workspaceService.DeleteWorkspaceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List changes and conflicts in a workspace.</summary>
    [HttpGet("{workspaceId}/status")]
    public async Task<IActionResult> GetWorkspaceStatusAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetWorkspaceStatusRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _workspaceService.GetWorkspaceStatusAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a container version from a workspace (ready to publish).</summary>
    [HttpPost("create-version")]
    public async Task<IActionResult> CreateVersionAsync([FromBody] CreateVersionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.CreateVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Sync a workspace to the latest container version.</summary>
    [HttpPost("sync")]
    public async Task<IActionResult> SyncWorkspaceAsync([FromBody] SyncWorkspaceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.SyncWorkspaceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a workspace.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateWorkspaceAsync([FromBody] UpdateWorkspaceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.UpdateWorkspaceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Quick-preview a workspace by creating a fake container version.</summary>
    [HttpPost("quick-preview")]
    public async Task<IActionResult> QuickPreviewAsync([FromBody] QuickPreviewRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.QuickPreviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Resolve a merge conflict for a workspace entity.</summary>
    [HttpPost("resolve-conflict")]
    public async Task<IActionResult> ResolveConflictAsync([FromBody] ResolveConflictRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _workspaceService.ResolveConflictAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

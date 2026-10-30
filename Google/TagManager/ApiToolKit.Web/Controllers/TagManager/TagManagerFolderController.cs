using ApiToolKit.Application.DTOs.TagManager.Folder;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace folders.
/// </summary>
[ApiController]
[Route("api/tagmanager/folders")]
[Produces("application/json")]
public class TagManagerFolderController : ControllerBase
{
    private readonly ITagManagerFolderService _folderService;

    public TagManagerFolderController(ITagManagerFolderService folderService)
    {
        _folderService = folderService;
    }

    /// <summary>List folders in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListFoldersAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListFoldersRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _folderService.ListFoldersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one folder.</summary>
    [HttpGet("{folderId}")]
    public async Task<IActionResult> GetFolderAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string folderId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetFolderRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, FolderId = folderId };
        var result = await _folderService.GetFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a folder.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateFolderAsync([FromBody] CreateFolderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _folderService.CreateFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a folder.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateFolderAsync([FromBody] UpdateFolderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _folderService.UpdateFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a folder.</summary>
    [HttpDelete("{folderId}")]
    public async Task<IActionResult> DeleteFolderAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string folderId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteFolderRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, FolderId = folderId };
        var result = await _folderService.DeleteFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a folder.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertFolderAsync([FromBody] RevertFolderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _folderService.RevertFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List tags, triggers and variables inside a folder.</summary>
    [HttpPost("entities")]
    public async Task<IActionResult> GetFolderEntitiesAsync([FromBody] GetFolderEntitiesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _folderService.GetFolderEntitiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Move tags, triggers and variables into a folder.</summary>
    [HttpPost("move-entities")]
    public async Task<IActionResult> MoveEntitiesToFolderAsync([FromBody] MoveEntitiesToFolderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _folderService.MoveEntitiesToFolderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

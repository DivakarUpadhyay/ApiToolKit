using ApiToolKit.Application.DTOs.TagManager.BuiltInVariable;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Built-in variables of a workspace.
/// </summary>
[ApiController]
[Route("api/tagmanager/built-in-variables")]
[Produces("application/json")]
public class TagManagerBuiltInVariableController : ControllerBase
{
    private readonly ITagManagerBuiltInVariableService _builtInVariableService;

    public TagManagerBuiltInVariableController(ITagManagerBuiltInVariableService builtInVariableService)
    {
        _builtInVariableService = builtInVariableService;
    }

    /// <summary>List enabled built-in variables.</summary>
    [HttpGet]
    public async Task<IActionResult> ListBuiltInVariablesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListBuiltInVariablesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _builtInVariableService.ListBuiltInVariablesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Enable built-in variables.</summary>
    [HttpPost]
    public async Task<IActionResult> EnableBuiltInVariablesAsync([FromBody] EnableBuiltInVariablesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _builtInVariableService.EnableBuiltInVariablesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Disable built-in variables.</summary>
    [HttpDelete]
    public async Task<IActionResult> DisableBuiltInVariablesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] List<string> types)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        if (types == null || types.Count == 0) return BadRequest("types is required.");
        var request = new DisableBuiltInVariablesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, Types = types };
        var result = await _builtInVariableService.DisableBuiltInVariablesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a built-in variable.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertBuiltInVariableAsync([FromBody] RevertBuiltInVariableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _builtInVariableService.RevertBuiltInVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

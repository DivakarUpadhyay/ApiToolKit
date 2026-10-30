using ApiToolKit.Application.DTOs.TagManager.Variable;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace variables.
/// </summary>
[ApiController]
[Route("api/tagmanager/variables")]
[Produces("application/json")]
public class TagManagerVariableController : ControllerBase
{
    private readonly ITagManagerVariableService _variableService;

    public TagManagerVariableController(ITagManagerVariableService variableService)
    {
        _variableService = variableService;
    }

    /// <summary>List variables in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListVariablesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListVariablesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _variableService.ListVariablesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one variable.</summary>
    [HttpGet("{variableId}")]
    public async Task<IActionResult> GetVariableAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string variableId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetVariableRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, VariableId = variableId };
        var result = await _variableService.GetVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a variable in a workspace.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateVariableAsync([FromBody] CreateVariableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _variableService.CreateVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a variable (full replace).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateVariableAsync([FromBody] UpdateVariableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _variableService.UpdateVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a variable.</summary>
    [HttpDelete("{variableId}")]
    public async Task<IActionResult> DeleteVariableAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string variableId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteVariableRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, VariableId = variableId };
        var result = await _variableService.DeleteVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a variable.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertVariableAsync([FromBody] RevertVariableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _variableService.RevertVariableAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

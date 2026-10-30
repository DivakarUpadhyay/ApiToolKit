using ApiToolKit.Application.DTOs.TagManager.Trigger;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace triggers.
/// </summary>
[ApiController]
[Route("api/tagmanager/triggers")]
[Produces("application/json")]
public class TagManagerTriggerController : ControllerBase
{
    private readonly ITagManagerTriggerService _triggerService;

    public TagManagerTriggerController(ITagManagerTriggerService triggerService)
    {
        _triggerService = triggerService;
    }

    /// <summary>List triggers in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListTriggersAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListTriggersRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _triggerService.ListTriggersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one trigger.</summary>
    [HttpGet("{triggerId}")]
    public async Task<IActionResult> GetTriggerAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string triggerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetTriggerRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TriggerId = triggerId };
        var result = await _triggerService.GetTriggerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a trigger in a workspace.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTriggerAsync([FromBody] CreateTriggerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _triggerService.CreateTriggerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a trigger (full replace).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTriggerAsync([FromBody] UpdateTriggerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _triggerService.UpdateTriggerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a trigger.</summary>
    [HttpDelete("{triggerId}")]
    public async Task<IActionResult> DeleteTriggerAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string triggerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteTriggerRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TriggerId = triggerId };
        var result = await _triggerService.DeleteTriggerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a trigger.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertTriggerAsync([FromBody] RevertTriggerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _triggerService.RevertTriggerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

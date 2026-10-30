using ApiToolKit.Application.DTOs.TagManager.Transformation;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace transformations.
/// </summary>
[ApiController]
[Route("api/tagmanager/transformations")]
[Produces("application/json")]
public class TagManagerTransformationController : ControllerBase
{
    private readonly ITagManagerTransformationService _transformationService;

    public TagManagerTransformationController(ITagManagerTransformationService transformationService)
    {
        _transformationService = transformationService;
    }

    /// <summary>List transformations in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListTransformationsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListTransformationsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _transformationService.ListTransformationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one transformation.</summary>
    [HttpGet("{transformationId}")]
    public async Task<IActionResult> GetTransformationAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string transformationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetTransformationRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TransformationId = transformationId };
        var result = await _transformationService.GetTransformationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a transformation.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTransformationAsync([FromBody] CreateTransformationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _transformationService.CreateTransformationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a transformation.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTransformationAsync([FromBody] UpdateTransformationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _transformationService.UpdateTransformationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a transformation.</summary>
    [HttpDelete("{transformationId}")]
    public async Task<IActionResult> DeleteTransformationAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string transformationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteTransformationRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TransformationId = transformationId };
        var result = await _transformationService.DeleteTransformationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a transformation.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertTransformationAsync([FromBody] RevertTransformationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _transformationService.RevertTransformationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

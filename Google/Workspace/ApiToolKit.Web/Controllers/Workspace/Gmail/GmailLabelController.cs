using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Gmail;

/// <summary>Gmail Labels API</summary>
[ApiController]
[Route("api/workspace/gmail/labels")]
public class GmailLabelController : ControllerBase
{
    private readonly IGmailLabelService _service;

    public GmailLabelController(IGmailLabelService service)
    {
        _service = service;
    }

    /// <summary>List all labels</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListLabels(string userId)
    {
        var result = await _service.ListLabelsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific label</summary>
    [HttpGet("{userId}/{labelId}")]
    public async Task<IActionResult> GetLabel(string userId, string labelId)
    {
        var result = await _service.GetLabelAsync(userId, labelId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new label</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateLabel(string userId, [FromBody] CreateLabelRequest request)
    {
        var result = await _service.CreateLabelAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing label</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateLabel(string userId, [FromBody] UpdateLabelRequest request)
    {
        var result = await _service.UpdateLabelAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a label</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteLabel(string userId, [FromBody] DeleteLabelRequest request)
    {
        var result = await _service.DeleteLabelAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

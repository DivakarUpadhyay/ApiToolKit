using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Gmail;

/// <summary>Gmail Threads API</summary>
[ApiController]
[Route("api/workspace/gmail/threads")]
public class GmailThreadController : ControllerBase
{
    private readonly IGmailThreadService _service;

    public GmailThreadController(IGmailThreadService service)
    {
        _service = service;
    }

    /// <summary>List threads</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListThreads(string userId, [FromQuery] ListThreadsRequest request)
    {
        var result = await _service.ListThreadsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific thread</summary>
    [HttpGet("{userId}/{threadId}")]
    public async Task<IActionResult> GetThread(string userId, string threadId)
    {
        var result = await _service.GetThreadAsync(userId, threadId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Modify thread labels</summary>
    [HttpPost("{userId}/modify")]
    public async Task<IActionResult> ModifyThread(string userId, [FromBody] ModifyThreadRequest request)
    {
        var result = await _service.ModifyThreadAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Trash a thread</summary>
    [HttpPost("{userId}/trash/{threadId}")]
    public async Task<IActionResult> TrashThread(string userId, string threadId)
    {
        var result = await _service.TrashThreadAsync(userId, threadId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Untrash a thread</summary>
    [HttpPost("{userId}/untrash/{threadId}")]
    public async Task<IActionResult> UntrashThread(string userId, string threadId)
    {
        var result = await _service.UntrashThreadAsync(userId, threadId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a thread</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteThread(string userId, [FromBody] DeleteThreadRequest request)
    {
        var result = await _service.DeleteThreadAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

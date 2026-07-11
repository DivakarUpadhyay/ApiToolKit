using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Gmail;

/// <summary>Gmail Drafts API</summary>
[ApiController]
[Route("api/workspace/gmail/drafts")]
public class GmailDraftController : ControllerBase
{
    private readonly IGmailDraftService _service;

    public GmailDraftController(IGmailDraftService service)
    {
        _service = service;
    }

    /// <summary>List all drafts</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListDrafts(string userId)
    {
        var result = await _service.ListDraftsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific draft</summary>
    [HttpGet("{userId}/{draftId}")]
    public async Task<IActionResult> GetDraft(string userId, string draftId)
    {
        var result = await _service.GetDraftAsync(userId, draftId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new draft</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateDraft(string userId, [FromBody] CreateDraftRequest request)
    {
        var result = await _service.CreateDraftAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing draft</summary>
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateDraft(string userId, [FromBody] UpdateDraftRequest request)
    {
        var result = await _service.UpdateDraftAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a draft</summary>
    [HttpPost("{userId}/send")]
    public async Task<IActionResult> SendDraft(string userId, [FromBody] SendDraftRequest request)
    {
        var result = await _service.SendDraftAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a draft</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteDraft(string userId, [FromBody] DeleteDraftRequest request)
    {
        var result = await _service.DeleteDraftAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

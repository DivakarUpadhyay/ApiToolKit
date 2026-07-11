using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Gmail;

/// <summary>Gmail Messages API</summary>
[ApiController]
[Route("api/workspace/gmail/messages")]
public class GmailMessageController : ControllerBase
{
    private readonly IGmailMessageService _service;

    public GmailMessageController(IGmailMessageService service)
    {
        _service = service;
    }

    /// <summary>List messages in the user's mailbox</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListMessages(string userId, [FromQuery] ListMessagesRequest request)
    {
        var result = await _service.ListMessagesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific message</summary>
    [HttpGet("{userId}/{messageId}")]
    public async Task<IActionResult> GetMessage(string userId, string messageId)
    {
        var result = await _service.GetMessageAsync(userId, messageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a new message</summary>
    [HttpPost("{userId}/send")]
    public async Task<IActionResult> SendMessage(string userId, [FromBody] SendMessageRequest request)
    {
        var result = await _service.SendMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Modify message labels</summary>
    [HttpPost("{userId}/modify")]
    public async Task<IActionResult> ModifyMessage(string userId, [FromBody] ModifyMessageRequest request)
    {
        var result = await _service.ModifyMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch modify multiple messages</summary>
    [HttpPost("{userId}/batchModify")]
    public async Task<IActionResult> BatchModify(string userId, [FromBody] BatchModifyRequest request)
    {
        var result = await _service.BatchModifyAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Trash a message</summary>
    [HttpPost("{userId}/trash")]
    public async Task<IActionResult> TrashMessage(string userId, [FromBody] TrashMessageRequest request)
    {
        var result = await _service.TrashMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Untrash a message</summary>
    [HttpPost("{userId}/untrash/{messageId}")]
    public async Task<IActionResult> UntrashMessage(string userId, string messageId)
    {
        var result = await _service.UntrashMessageAsync(userId, messageId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a message attachment</summary>
    [HttpGet("{userId}/attachment")]
    public async Task<IActionResult> GetAttachment(string userId, [FromQuery] GetAttachmentRequest request)
    {
        var result = await _service.GetAttachmentAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a message permanently</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteMessage(string userId, [FromBody] DeleteMessageRequest request)
    {
        var result = await _service.DeleteMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

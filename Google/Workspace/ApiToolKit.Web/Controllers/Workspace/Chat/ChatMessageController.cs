using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Chat;

/// <summary>Google Chat Messages API</summary>
[ApiController]
[Route("api/workspace/chat/messages")]
public class ChatMessageController : ControllerBase
{
    private readonly IChatMessageService _service;

    public ChatMessageController(IChatMessageService service)
    {
        _service = service;
    }

    /// <summary>List messages in a Chat space</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListMessages(string userId, [FromQuery] ListMessagesRequest request)
    {
        var result = await _service.ListMessagesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific message</summary>
    [HttpGet("{userId}/{messageName}")]
    public async Task<IActionResult> GetMessage(string userId, string messageName)
    {
        var result = await _service.GetMessageAsync(userId, messageName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a message to a Chat space</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> SendMessage(string userId, [FromBody] SendMessageRequest request)
    {
        var result = await _service.SendMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a message</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateMessage(string userId, [FromBody] UpdateMessageRequest request)
    {
        var result = await _service.UpdateMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a message</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteMessage(string userId, [FromBody] DeleteMessageRequest request)
    {
        var result = await _service.DeleteMessageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

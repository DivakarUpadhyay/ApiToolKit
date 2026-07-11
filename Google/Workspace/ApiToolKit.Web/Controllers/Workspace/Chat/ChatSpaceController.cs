using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Chat;

/// <summary>Google Chat Spaces API</summary>
[ApiController]
[Route("api/workspace/chat/spaces")]
public class ChatSpaceController : ControllerBase
{
    private readonly IChatSpaceService _service;

    public ChatSpaceController(IChatSpaceService service)
    {
        _service = service;
    }

    /// <summary>List Chat spaces</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListSpaces(string userId)
    {
        var result = await _service.ListSpacesAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific Chat space</summary>
    [HttpGet("{userId}/{spaceName}")]
    public async Task<IActionResult> GetSpace(string userId, string spaceName)
    {
        var result = await _service.GetSpaceAsync(userId, spaceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new Chat space</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateSpace(string userId, [FromBody] CreateChatSpaceRequest request)
    {
        var result = await _service.CreateSpaceAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a Chat space</summary>
    [HttpDelete("{userId}/{spaceName}")]
    public async Task<IActionResult> DeleteSpace(string userId, string spaceName)
    {
        var result = await _service.DeleteSpaceAsync(userId, spaceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

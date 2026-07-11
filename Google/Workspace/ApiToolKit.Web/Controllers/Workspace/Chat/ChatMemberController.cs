using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Chat;

/// <summary>Google Chat Members API</summary>
[ApiController]
[Route("api/workspace/chat/members")]
public class ChatMemberController : ControllerBase
{
    private readonly IChatMemberService _service;

    public ChatMemberController(IChatMemberService service)
    {
        _service = service;
    }

    /// <summary>List members of a Chat space</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListMembers(string userId, [FromQuery] ListMembersRequest request)
    {
        var result = await _service.ListMembersAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific member</summary>
    [HttpGet("{userId}/{membershipName}")]
    public async Task<IActionResult> GetMember(string userId, string membershipName)
    {
        var result = await _service.GetMemberAsync(userId, membershipName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a member to a Chat space</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateMember(string userId, [FromBody] CreateMemberRequest request)
    {
        var result = await _service.CreateMemberAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a member from a Chat space</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteMember(string userId, [FromBody] DeleteMemberRequest request)
    {
        var result = await _service.DeleteMemberAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

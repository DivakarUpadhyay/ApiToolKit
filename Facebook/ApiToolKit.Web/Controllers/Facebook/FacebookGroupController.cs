using ApiToolKit.Application.DTOs.Facebook.Group;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/group")]
[Produces("application/json")]
public class FacebookGroupController : ControllerBase
{
    private readonly IFacebookGroupService _groupService;

    public FacebookGroupController(IFacebookGroupService groupService)
    {
        _groupService = groupService;
    }

    /// <summary>Get group details by group ID</summary>
    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupAsync(string groupId, [FromQuery] string accessToken)
    {
        var result = await _groupService.GetGroupAsync(groupId, accessToken);
        return Ok(result);
    }

    /// <summary>Get groups that a user belongs to</summary>
    [HttpGet("user/{userId}/groups")]
    public async Task<IActionResult> GetGroupsByUserAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _groupService.GetGroupsByUserAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get the post feed of a group</summary>
    [HttpPost("{groupId}/feed")]
    public async Task<IActionResult> GetGroupFeedAsync([FromBody] GroupFeedRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _groupService.GetGroupFeedAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get members of a group</summary>
    [HttpPost("{groupId}/members")]
    public async Task<IActionResult> GetGroupMembersAsync([FromBody] GroupMembersRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _groupService.GetGroupMembersAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Create a post in a group</summary>
    [HttpPost("{groupId}/post")]
    public async Task<IActionResult> CreateGroupPostAsync([FromBody] CreateGroupPostRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _groupService.CreateGroupPostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get albums in a group</summary>
    [HttpPost("{groupId}/albums")]
    public async Task<IActionResult> GetGroupAlbumsAsync([FromBody] GroupAlbumsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _groupService.GetGroupAlbumsAsync(request, accessToken);
        return Ok(result);
    }
}

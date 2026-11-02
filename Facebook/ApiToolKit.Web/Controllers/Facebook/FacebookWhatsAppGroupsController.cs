using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/groups")]
[Produces("application/json")]
public class FacebookWhatsAppGroupsController : ControllerBase
{
    private readonly IFacebookWhatsAppGroupsService _service;

    public FacebookWhatsAppGroupsController(IFacebookWhatsAppGroupsService service) => _service = service;

    /// <summary>Create a group</summary>
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] WaCreateGroupRequest request) =>
        Ok(await _service.CreateGroupAsync(request));

    /// <summary>List the business phone number's groups</summary>
    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] string phoneNumberId, [FromQuery] string accessToken,
        [FromQuery] int? limit, [FromQuery] string? after, [FromQuery] string? before) =>
        Ok(await _service.GetGroupsAsync(phoneNumberId, accessToken, limit, after, before));

    /// <summary>Get group info (subject, description, participants …)</summary>
    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroup(string groupId, [FromQuery] string accessToken, [FromQuery] string? fields) =>
        Ok(await _service.GetGroupAsync(groupId, accessToken, fields));

    /// <summary>Update group subject, description or profile picture</summary>
    [HttpPost("{groupId}")]
    public async Task<IActionResult> UpdateGroup(string groupId, [FromBody] WaUpdateGroupRequest request) =>
        Ok(await _service.UpdateGroupAsync(groupId, request));

    /// <summary>Delete a group (removes all participants)</summary>
    [HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteGroup(string groupId, [FromQuery] string accessToken) =>
        Ok(await _service.DeleteGroupAsync(groupId, accessToken));

    /// <summary>Get the group invite link</summary>
    [HttpGet("{groupId}/invite-link")]
    public async Task<IActionResult> GetInviteLink(string groupId, [FromQuery] string accessToken) =>
        Ok(await _service.GetInviteLinkAsync(groupId, accessToken));

    /// <summary>Reset the group invite link (old links stop working)</summary>
    [HttpPost("{groupId}/invite-link")]
    public async Task<IActionResult> ResetInviteLink(string groupId, [FromQuery] string accessToken) =>
        Ok(await _service.ResetInviteLinkAsync(groupId, accessToken));

    /// <summary>Remove up to 8 participants</summary>
    [HttpDelete("{groupId}/participants")]
    public async Task<IActionResult> RemoveParticipants(string groupId, [FromBody] WaGroupParticipantsRequest request) =>
        Ok(await _service.RemoveParticipantsAsync(groupId, request));

    /// <summary>List pending join requests</summary>
    [HttpGet("{groupId}/join-requests")]
    public async Task<IActionResult> GetJoinRequests(string groupId, [FromQuery] string accessToken) =>
        Ok(await _service.GetJoinRequestsAsync(groupId, accessToken));

    /// <summary>Approve join requests</summary>
    [HttpPost("{groupId}/join-requests")]
    public async Task<IActionResult> ApproveJoinRequests(string groupId, [FromBody] WaJoinRequestsRequest request) =>
        Ok(await _service.ApproveJoinRequestsAsync(groupId, request));

    /// <summary>Reject join requests</summary>
    [HttpDelete("{groupId}/join-requests")]
    public async Task<IActionResult> RejectJoinRequests(string groupId, [FromBody] WaJoinRequestsRequest request) =>
        Ok(await _service.RejectJoinRequestsAsync(groupId, request));

    /// <summary>Send a message to a group</summary>
    [HttpPost("{groupId}/messages")]
    public async Task<IActionResult> SendGroupMessage(string groupId, [FromBody] WaGroupMessageRequest request) =>
        Ok(await _service.SendGroupMessageAsync(groupId, request));

    /// <summary>Send a template carrying the group invite link to a user</summary>
    [HttpPost("invite-template")]
    public async Task<IActionResult> SendGroupInviteTemplate([FromBody] WaGroupInviteTemplateRequest request) =>
        Ok(await _service.SendGroupInviteTemplateAsync(request));
}

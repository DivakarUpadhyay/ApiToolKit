using ApiToolKit.Application.DTOs.BusinessProfile.Account;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Business Profile accounts (Account Management API).
/// </summary>
[ApiController]
[Route("api/businessprofile/accounts")]
[Produces("application/json")]
public class BusinessProfileAccountController : ControllerBase
{
    private readonly IBusinessProfileAccountService _accountService;

    public BusinessProfileAccountController(IBusinessProfileAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>List Business Profile accounts for the signed-in user.</summary>
    [HttpGet]
    public async Task<IActionResult> ListAccountsAsync([FromQuery] string accountKey, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountsRequest { AccountKey = accountKey, PageSize = pageSize, PageToken = pageToken };
        var result = await _accountService.ListAccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one Business Profile account.</summary>
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _accountService.GetAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a location group or organization account.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.CreateAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an account (fields in UpdateMask).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAccountAsync([FromBody] UpdateAccountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List admins of an account.</summary>
    [HttpGet("{accountId}/admins")]
    public async Task<IActionResult> ListAccountAdminsAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountAdminsRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _accountService.ListAccountAdminsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Invite a user to be an admin of an account.</summary>
    [HttpPost("admins")]
    public async Task<IActionResult> InviteAccountAdminAsync([FromBody] InviteAccountAdminRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.InviteAccountAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Change the role of an account admin.</summary>
    [HttpPut("admins")]
    public async Task<IActionResult> UpdateAccountAdminAsync([FromBody] UpdateAccountAdminRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateAccountAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove an admin from an account.</summary>
    [HttpDelete("{accountId}/admins/{adminId}")]
    public async Task<IActionResult> RemoveAccountAdminAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string adminId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RemoveAccountAdminRequest { AccountKey = accountKey, AccountId = accountId, AdminId = adminId };
        var result = await _accountService.RemoveAccountAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List pending invitations for an account.</summary>
    [HttpGet("{accountId}/invitations")]
    public async Task<IActionResult> ListInvitationsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListInvitationsRequest { AccountKey = accountKey, AccountId = accountId, Filter = filter };
        var result = await _accountService.ListInvitationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Accept an invitation.</summary>
    [HttpPost("invitations/accept")]
    public async Task<IActionResult> AcceptInvitationAsync([FromBody] AcceptInvitationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.AcceptInvitationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Decline an invitation.</summary>
    [HttpPost("invitations/decline")]
    public async Task<IActionResult> DeclineInvitationAsync([FromBody] DeclineInvitationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.DeclineInvitationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the Pub/Sub notification setting of an account (Notifications API).</summary>
    [HttpGet("{accountId}/notification-setting")]
    public async Task<IActionResult> GetNotificationSettingAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetNotificationSettingRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _accountService.GetNotificationSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set the Pub/Sub topic and notification types for an account.</summary>
    [HttpPut("notification-setting")]
    public async Task<IActionResult> UpdateNotificationSettingAsync([FromBody] UpdateNotificationSettingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateNotificationSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

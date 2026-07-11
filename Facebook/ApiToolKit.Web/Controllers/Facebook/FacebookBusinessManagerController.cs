using ApiToolKit.Application.DTOs.Facebook.BusinessManager;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/business")]
[Produces("application/json")]
public class FacebookBusinessManagerController : ControllerBase {
    private readonly IFacebookBusinessManagerService _service;
    public FacebookBusinessManagerController(IFacebookBusinessManagerService service) => _service = service;

    /// <summary>Get details of a Meta Business Manager account</summary>
    [HttpGet("{businessId}")]
    public async Task<IActionResult> GetBusiness(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetBusinessAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all owned Facebook Pages for a Business Manager</summary>
    [HttpGet("{businessId}/pages")]
    public async Task<IActionResult> GetOwnedPages(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetOwnedPagesAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all owned ad accounts for a Business Manager</summary>
    [HttpGet("{businessId}/ad-accounts")]
    public async Task<IActionResult> GetOwnedAdAccounts(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetOwnedAdAccountsAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all owned Instagram accounts for a Business Manager</summary>
    [HttpGet("{businessId}/instagram-accounts")]
    public async Task<IActionResult> GetOwnedInstagramAccounts(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetOwnedInstagramAccountsAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all owned product catalogs for a Business Manager</summary>
    [HttpGet("{businessId}/catalogs")]
    public async Task<IActionResult> GetOwnedCatalogs(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetOwnedCatalogsAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all users associated with a Business Manager</summary>
    [HttpGet("{businessId}/users")]
    public async Task<IActionResult> GetBusinessUsers(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetBusinessUsersAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Invite a user to a Business Manager by email</summary>
    [HttpPost("{businessId}/users")]
    public async Task<IActionResult> InviteBusinessUser(string businessId, [FromBody] InviteBusinessUserRequest request, [FromQuery] string accessToken) {
        var result = await _service.InviteBusinessUserAsync(businessId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Remove a user from a Business Manager</summary>
    [HttpDelete("{businessId}/users/{userId}")]
    public async Task<IActionResult> RemoveBusinessUser(string businessId, string userId, [FromQuery] string accessToken) {
        var result = await _service.RemoveBusinessUserAsync(businessId, userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all system users for a Business Manager</summary>
    [HttpGet("{businessId}/system-users")]
    public async Task<IActionResult> GetSystemUsers(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetSystemUsersAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a new system user in a Business Manager</summary>
    [HttpPost("{businessId}/system-users")]
    public async Task<IActionResult> CreateSystemUser(string businessId, [FromBody] CreateSystemUserRequest request, [FromQuery] string accessToken) {
        var result = await _service.CreateSystemUserAsync(businessId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get all client pages associated with a Business Manager</summary>
    [HttpGet("{businessId}/client-pages")]
    public async Task<IActionResult> GetClientPages(string businessId, [FromQuery] string accessToken) {
        var result = await _service.GetClientPagesAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Claim a Facebook Page into a Business Manager</summary>
    [HttpPost("{businessId}/claim-page")]
    public async Task<IActionResult> ClaimPage(string businessId, [FromBody] ClaimPageRequest request, [FromQuery] string accessToken) {
        var result = await _service.ClaimPageAsync(businessId, request, accessToken);
        return Ok(result);
    }
}

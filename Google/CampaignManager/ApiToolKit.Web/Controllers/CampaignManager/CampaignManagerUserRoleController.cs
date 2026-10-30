using ApiToolKit.Application.DTOs.CampaignManager.UserRole;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// userRoles (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerUserRoleController : ControllerBase
{
    private readonly ICampaignManagerUserRoleService _userRoleService;

    public CampaignManagerUserRoleController(ICampaignManagerUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    /// <summary>Retrieves a list of user roles, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRoles")]
    public async Task<IActionResult> ListUserRolesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortOrder = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] bool? accountUserRoleOnly = null, [FromQuery] string? sortField = null, [FromQuery] string? subaccountId = null, [FromQuery] List<string>? ids = null, [FromQuery] int? maxResults = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUserRolesRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortOrder = sortOrder, SearchString = searchString, PageToken = pageToken, AccountUserRoleOnly = accountUserRoleOnly, SortField = sortField, SubaccountId = subaccountId, Ids = ids, MaxResults = maxResults };
        var result = await _userRoleService.ListUserRolesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new user role.</summary>
    [HttpPost("userprofiles/{userprofileId}/userRoles")]
    public async Task<IActionResult> InsertUserRoleAsync([FromRoute] string userprofileId, [FromBody] InsertUserRoleRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userRoleService.InsertUserRoleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing user role.</summary>
    [HttpPatch("userprofiles/{userprofileId}/userRoles")]
    public async Task<IActionResult> UpdateUserRoleAsync([FromRoute] string userprofileId, [FromBody] UpdateUserRoleRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userRoleService.UpdateUserRoleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one user role by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRoles/{userRoleId}")]
    public async Task<IActionResult> GetUserRoleAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string userRoleId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserRoleRequest { AccountKey = accountKey, UserprofileId = userprofileId, UserRoleId = userRoleId };
        var result = await _userRoleService.GetUserRoleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing user role.</summary>
    [HttpDelete("userprofiles/{userprofileId}/userRoles/{userRoleId}")]
    public async Task<IActionResult> DeleteUserRoleAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string userRoleId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteUserRoleRequest { AccountKey = accountKey, UserprofileId = userprofileId, UserRoleId = userRoleId };
        var result = await _userRoleService.DeleteUserRoleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing user role.</summary>
    [HttpPut("userprofiles/{userprofileId}/userRoles")]
    public async Task<IActionResult> UpdateUserRoleAltAsync([FromRoute] string userprofileId, [FromBody] UpdateUserRoleAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userRoleService.UpdateUserRoleAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

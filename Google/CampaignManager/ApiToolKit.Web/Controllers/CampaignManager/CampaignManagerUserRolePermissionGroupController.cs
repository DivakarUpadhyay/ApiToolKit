using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermissionGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// userRolePermissionGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerUserRolePermissionGroupController : ControllerBase
{
    private readonly ICampaignManagerUserRolePermissionGroupService _userRolePermissionGroupService;

    public CampaignManagerUserRolePermissionGroupController(ICampaignManagerUserRolePermissionGroupService userRolePermissionGroupService)
    {
        _userRolePermissionGroupService = userRolePermissionGroupService;
    }

    /// <summary>Gets a list of all supported user role permission groups.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRolePermissionGroups")]
    public async Task<IActionResult> ListUserRolePermissionGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUserRolePermissionGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _userRolePermissionGroupService.ListUserRolePermissionGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one user role permission group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRolePermissionGroups/{userRolePermissionGroupId}")]
    public async Task<IActionResult> GetUserRolePermissionGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string userRolePermissionGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserRolePermissionGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, UserRolePermissionGroupId = userRolePermissionGroupId };
        var result = await _userRolePermissionGroupService.GetUserRolePermissionGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

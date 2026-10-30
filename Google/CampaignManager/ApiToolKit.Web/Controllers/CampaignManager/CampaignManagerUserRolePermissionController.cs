using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermission;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// userRolePermissions (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerUserRolePermissionController : ControllerBase
{
    private readonly ICampaignManagerUserRolePermissionService _userRolePermissionService;

    public CampaignManagerUserRolePermissionController(ICampaignManagerUserRolePermissionService userRolePermissionService)
    {
        _userRolePermissionService = userRolePermissionService;
    }

    /// <summary>Gets one user role permission by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRolePermissions/{userRolePermissionId}")]
    public async Task<IActionResult> GetUserRolePermissionAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string userRolePermissionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserRolePermissionRequest { AccountKey = accountKey, UserprofileId = userprofileId, UserRolePermissionId = userRolePermissionId };
        var result = await _userRolePermissionService.GetUserRolePermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a list of user role permissions, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/userRolePermissions")]
    public async Task<IActionResult> ListUserRolePermissionsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? ids = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUserRolePermissionsRequest { AccountKey = accountKey, UserprofileId = userprofileId, Ids = ids };
        var result = await _userRolePermissionService.ListUserRolePermissionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

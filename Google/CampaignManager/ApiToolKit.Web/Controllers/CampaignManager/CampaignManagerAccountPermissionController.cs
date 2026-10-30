using ApiToolKit.Application.DTOs.CampaignManager.AccountPermission;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// accountPermissions (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAccountPermissionController : ControllerBase
{
    private readonly ICampaignManagerAccountPermissionService _accountPermissionService;

    public CampaignManagerAccountPermissionController(ICampaignManagerAccountPermissionService accountPermissionService)
    {
        _accountPermissionService = accountPermissionService;
    }

    /// <summary>Gets one account permission by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountPermissions/{accountPermissionId}")]
    public async Task<IActionResult> GetAccountPermissionAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string accountPermissionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountPermissionRequest { AccountKey = accountKey, UserprofileId = userprofileId, AccountPermissionId = accountPermissionId };
        var result = await _accountPermissionService.GetAccountPermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of account permissions.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountPermissions")]
    public async Task<IActionResult> ListAccountPermissionsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountPermissionsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _accountPermissionService.ListAccountPermissionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

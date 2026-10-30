using ApiToolKit.Application.DTOs.CampaignManager.AccountPermissionGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// accountPermissionGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAccountPermissionGroupController : ControllerBase
{
    private readonly ICampaignManagerAccountPermissionGroupService _accountPermissionGroupService;

    public CampaignManagerAccountPermissionGroupController(ICampaignManagerAccountPermissionGroupService accountPermissionGroupService)
    {
        _accountPermissionGroupService = accountPermissionGroupService;
    }

    /// <summary>Gets one account permission group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountPermissionGroups/{accountPermissionGroupId}")]
    public async Task<IActionResult> GetAccountPermissionGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string accountPermissionGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountPermissionGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, AccountPermissionGroupId = accountPermissionGroupId };
        var result = await _accountPermissionGroupService.GetAccountPermissionGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of account permission groups.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountPermissionGroups")]
    public async Task<IActionResult> ListAccountPermissionGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountPermissionGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _accountPermissionGroupService.ListAccountPermissionGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

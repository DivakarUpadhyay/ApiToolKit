using ApiToolKit.Application.DTOs.CampaignManager.MobileApp;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// mobileApps (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerMobileAppController : ControllerBase
{
    private readonly ICampaignManagerMobileAppService _mobileAppService;

    public CampaignManagerMobileAppController(ICampaignManagerMobileAppService mobileAppService)
    {
        _mobileAppService = mobileAppService;
    }

    /// <summary>Gets one mobile app by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/mobileApps/{mobileAppId}")]
    public async Task<IActionResult> GetMobileAppAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string mobileAppId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetMobileAppRequest { AccountKey = accountKey, UserprofileId = userprofileId, MobileAppId = mobileAppId };
        var result = await _mobileAppService.GetMobileAppAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves list of available mobile apps.</summary>
    [HttpGet("userprofiles/{userprofileId}/mobileApps")]
    public async Task<IActionResult> ListMobileAppsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? directories = null, [FromQuery] int? maxResults = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? ids = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListMobileAppsRequest { AccountKey = accountKey, UserprofileId = userprofileId, Directories = directories, MaxResults = maxResults, SearchString = searchString, PageToken = pageToken, Ids = ids };
        var result = await _mobileAppService.ListMobileAppsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

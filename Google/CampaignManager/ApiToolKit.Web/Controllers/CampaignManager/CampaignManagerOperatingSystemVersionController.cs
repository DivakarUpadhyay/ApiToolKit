using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystemVersion;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// operatingSystemVersions (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerOperatingSystemVersionController : ControllerBase
{
    private readonly ICampaignManagerOperatingSystemVersionService _operatingSystemVersionService;

    public CampaignManagerOperatingSystemVersionController(ICampaignManagerOperatingSystemVersionService operatingSystemVersionService)
    {
        _operatingSystemVersionService = operatingSystemVersionService;
    }

    /// <summary>Gets one operating system version by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/operatingSystemVersions/{operatingSystemVersionId}")]
    public async Task<IActionResult> GetOperatingSystemVersionAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string operatingSystemVersionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOperatingSystemVersionRequest { AccountKey = accountKey, UserprofileId = userprofileId, OperatingSystemVersionId = operatingSystemVersionId };
        var result = await _operatingSystemVersionService.GetOperatingSystemVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of operating system versions.</summary>
    [HttpGet("userprofiles/{userprofileId}/operatingSystemVersions")]
    public async Task<IActionResult> ListOperatingSystemVersionsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListOperatingSystemVersionsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _operatingSystemVersionService.ListOperatingSystemVersionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

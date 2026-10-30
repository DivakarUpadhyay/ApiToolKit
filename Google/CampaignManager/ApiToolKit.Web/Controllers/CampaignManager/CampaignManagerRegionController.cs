using ApiToolKit.Application.DTOs.CampaignManager.Region;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// regions (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerRegionController : ControllerBase
{
    private readonly ICampaignManagerRegionService _regionService;

    public CampaignManagerRegionController(ICampaignManagerRegionService regionService)
    {
        _regionService = regionService;
    }

    /// <summary>Retrieves a list of regions.</summary>
    [HttpGet("userprofiles/{userprofileId}/regions")]
    public async Task<IActionResult> ListRegionsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListRegionsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _regionService.ListRegionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.FloodlightConfiguration;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// floodlightConfigurations (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerFloodlightConfigurationController : ControllerBase
{
    private readonly ICampaignManagerFloodlightConfigurationService _floodlightConfigurationService;

    public CampaignManagerFloodlightConfigurationController(ICampaignManagerFloodlightConfigurationService floodlightConfigurationService)
    {
        _floodlightConfigurationService = floodlightConfigurationService;
    }

    /// <summary>Gets one floodlight configuration by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightConfigurations/{floodlightConfigurationId}")]
    public async Task<IActionResult> GetFloodlightConfigurationAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string floodlightConfigurationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFloodlightConfigurationRequest { AccountKey = accountKey, UserprofileId = userprofileId, FloodlightConfigurationId = floodlightConfigurationId };
        var result = await _floodlightConfigurationService.GetFloodlightConfigurationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing floodlight configuration.</summary>
    [HttpPut("userprofiles/{userprofileId}/floodlightConfigurations")]
    public async Task<IActionResult> UpdateFloodlightConfigurationAsync([FromRoute] string userprofileId, [FromBody] UpdateFloodlightConfigurationRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightConfigurationService.UpdateFloodlightConfigurationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of floodlight configurations, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightConfigurations")]
    public async Task<IActionResult> ListFloodlightConfigurationsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? ids = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFloodlightConfigurationsRequest { AccountKey = accountKey, UserprofileId = userprofileId, Ids = ids };
        var result = await _floodlightConfigurationService.ListFloodlightConfigurationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing floodlight configuration.</summary>
    [HttpPatch("userprofiles/{userprofileId}/floodlightConfigurations")]
    public async Task<IActionResult> PatchFloodlightConfigurationAsync([FromRoute] string userprofileId, [FromBody] PatchFloodlightConfigurationRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightConfigurationService.PatchFloodlightConfigurationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

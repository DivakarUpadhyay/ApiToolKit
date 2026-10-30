using ApiToolKit.Application.DTOs.CampaignManager.DynamicProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// dynamicProfiles (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerDynamicProfileController : ControllerBase
{
    private readonly ICampaignManagerDynamicProfileService _dynamicProfileService;

    public CampaignManagerDynamicProfileController(ICampaignManagerDynamicProfileService dynamicProfileService)
    {
        _dynamicProfileService = dynamicProfileService;
    }

    /// <summary>Generates code for a dynamic profile, which will need unescaping.</summary>
    [HttpGet("studio/dynamicProfiles/{dynamicProfileId}/generateCode")]
    public async Task<IActionResult> GenerateCodeDynamicProfileAsync([FromQuery] string accountKey, [FromRoute] string dynamicProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GenerateCodeDynamicProfileRequest { AccountKey = accountKey, DynamicProfileId = dynamicProfileId };
        var result = await _dynamicProfileService.GenerateCodeDynamicProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing dynamic profile.</summary>
    [HttpPut("studio/dynamicProfiles")]
    public async Task<IActionResult> UpdateDynamicProfileAsync([FromBody] UpdateDynamicProfileRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicProfileService.UpdateDynamicProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new dynamic profile.</summary>
    [HttpPost("studio/dynamicProfiles")]
    public async Task<IActionResult> InsertDynamicProfileAsync([FromBody] InsertDynamicProfileRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicProfileService.InsertDynamicProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Publish for a dynamic profile.</summary>
    [HttpPost("studio/dynamicProfiles/{dynamicProfileId}/publish")]
    public async Task<IActionResult> PublishDynamicProfileAsync([FromRoute] string dynamicProfileId, [FromBody] PublishDynamicProfileRequest request)
    {
        request.DynamicProfileId = dynamicProfileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicProfileService.PublishDynamicProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a dynamic profile by ID.</summary>
    [HttpGet("studio/dynamicProfiles/{dynamicProfileId}")]
    public async Task<IActionResult> GetDynamicProfileAsync([FromQuery] string accountKey, [FromRoute] string dynamicProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetDynamicProfileRequest { AccountKey = accountKey, DynamicProfileId = dynamicProfileId };
        var result = await _dynamicProfileService.GetDynamicProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

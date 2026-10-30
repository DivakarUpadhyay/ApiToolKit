using ApiToolKit.Application.DTOs.CampaignManager.Conversion;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// conversions (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerConversionController : ControllerBase
{
    private readonly ICampaignManagerConversionService _conversionService;

    public CampaignManagerConversionController(ICampaignManagerConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    /// <summary>Updates existing conversions.</summary>
    [HttpPost("userprofiles/{profileId}/conversions/batchupdate")]
    public async Task<IActionResult> BatchupdateConversionAsync([FromRoute] string profileId, [FromBody] BatchupdateConversionRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionService.BatchupdateConversionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts conversions.</summary>
    [HttpPost("userprofiles/{profileId}/conversions/batchinsert")]
    public async Task<IActionResult> BatchinsertConversionAsync([FromRoute] string profileId, [FromBody] BatchinsertConversionRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionService.BatchinsertConversionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

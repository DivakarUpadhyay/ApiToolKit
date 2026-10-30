using ApiToolKit.Application.DTOs.CampaignManager.StudioCreative;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// studioCreatives (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerStudioCreativeController : ControllerBase
{
    private readonly ICampaignManagerStudioCreativeService _studioCreativeService;

    public CampaignManagerStudioCreativeController(ICampaignManagerStudioCreativeService studioCreativeService)
    {
        _studioCreativeService = studioCreativeService;
    }

    /// <summary>Gets a studio creative by ID.</summary>
    [HttpGet("studio/creatives/{creativeId}")]
    public async Task<IActionResult> GetStudioCreativeAsync([FromQuery] string accountKey, [FromRoute] string creativeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetStudioCreativeRequest { AccountKey = accountKey, CreativeId = creativeId };
        var result = await _studioCreativeService.GetStudioCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new studio creative.</summary>
    [HttpPost("studio/creatives")]
    public async Task<IActionResult> InsertStudioCreativeAsync([FromBody] InsertStudioCreativeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _studioCreativeService.InsertStudioCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Publish for a studio creative.</summary>
    [HttpPost("studio/creatives/{creativeId}/publish")]
    public async Task<IActionResult> PublishStudioCreativeAsync([FromRoute] string creativeId, [FromBody] PublishStudioCreativeRequest request)
    {
        request.CreativeId = creativeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _studioCreativeService.PublishStudioCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.PlatformType;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// platformTypes (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerPlatformTypeController : ControllerBase
{
    private readonly ICampaignManagerPlatformTypeService _platformTypeService;

    public CampaignManagerPlatformTypeController(ICampaignManagerPlatformTypeService platformTypeService)
    {
        _platformTypeService = platformTypeService;
    }

    /// <summary>Gets one platform type by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/platformTypes/{platformTypeId}")]
    public async Task<IActionResult> GetPlatformTypeAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string platformTypeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPlatformTypeRequest { AccountKey = accountKey, UserprofileId = userprofileId, PlatformTypeId = platformTypeId };
        var result = await _platformTypeService.GetPlatformTypeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of platform types.</summary>
    [HttpGet("userprofiles/{userprofileId}/platformTypes")]
    public async Task<IActionResult> ListPlatformTypesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPlatformTypesRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _platformTypeService.ListPlatformTypesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

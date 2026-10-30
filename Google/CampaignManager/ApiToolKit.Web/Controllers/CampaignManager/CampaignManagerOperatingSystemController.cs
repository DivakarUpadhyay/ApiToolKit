using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystem;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// operatingSystems (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerOperatingSystemController : ControllerBase
{
    private readonly ICampaignManagerOperatingSystemService _operatingSystemService;

    public CampaignManagerOperatingSystemController(ICampaignManagerOperatingSystemService operatingSystemService)
    {
        _operatingSystemService = operatingSystemService;
    }

    /// <summary>Gets one operating system by DART ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/operatingSystems/{operatingSystemId}")]
    public async Task<IActionResult> GetOperatingSystemAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string operatingSystemId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOperatingSystemRequest { AccountKey = accountKey, UserprofileId = userprofileId, OperatingSystemId = operatingSystemId };
        var result = await _operatingSystemService.GetOperatingSystemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of operating systems.</summary>
    [HttpGet("userprofiles/{userprofileId}/operatingSystems")]
    public async Task<IActionResult> ListOperatingSystemsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListOperatingSystemsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _operatingSystemService.ListOperatingSystemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

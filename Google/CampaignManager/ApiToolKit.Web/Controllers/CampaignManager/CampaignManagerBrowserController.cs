using ApiToolKit.Application.DTOs.CampaignManager.Browser;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// browsers (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerBrowserController : ControllerBase
{
    private readonly ICampaignManagerBrowserService _browserService;

    public CampaignManagerBrowserController(ICampaignManagerBrowserService browserService)
    {
        _browserService = browserService;
    }

    /// <summary>Retrieves a list of browsers.</summary>
    [HttpGet("userprofiles/{userprofileId}/browsers")]
    public async Task<IActionResult> ListBrowsersAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListBrowsersRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _browserService.ListBrowsersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

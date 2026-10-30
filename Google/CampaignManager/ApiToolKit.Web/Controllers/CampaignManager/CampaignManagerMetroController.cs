using ApiToolKit.Application.DTOs.CampaignManager.Metro;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// metros (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerMetroController : ControllerBase
{
    private readonly ICampaignManagerMetroService _metroService;

    public CampaignManagerMetroController(ICampaignManagerMetroService metroService)
    {
        _metroService = metroService;
    }

    /// <summary>Retrieves a list of metros.</summary>
    [HttpGet("userprofiles/{userprofileId}/metros")]
    public async Task<IActionResult> ListMetrosAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListMetrosRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _metroService.ListMetrosAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

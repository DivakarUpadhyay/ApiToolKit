using ApiToolKit.Application.DTOs.CampaignManager.Language;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// languages (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerLanguageController : ControllerBase
{
    private readonly ICampaignManagerLanguageService _languageService;

    public CampaignManagerLanguageController(ICampaignManagerLanguageService languageService)
    {
        _languageService = languageService;
    }

    /// <summary>Retrieves a list of languages.</summary>
    [HttpGet("userprofiles/{userprofileId}/languages")]
    public async Task<IActionResult> ListLanguagesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLanguagesRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _languageService.ListLanguagesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

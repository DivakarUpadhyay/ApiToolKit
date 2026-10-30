using ApiToolKit.Application.DTOs.CampaignManager.Country;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// countries (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCountryController : ControllerBase
{
    private readonly ICampaignManagerCountryService _countryService;

    public CampaignManagerCountryController(ICampaignManagerCountryService countryService)
    {
        _countryService = countryService;
    }

    /// <summary>Gets one country by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/countries/{countryId}")]
    public async Task<IActionResult> GetCountryAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string countryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCountryRequest { AccountKey = accountKey, UserprofileId = userprofileId, CountryId = countryId };
        var result = await _countryService.GetCountryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of countries.</summary>
    [HttpGet("userprofiles/{userprofileId}/countries")]
    public async Task<IActionResult> ListCountriesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCountriesRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _countryService.ListCountriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

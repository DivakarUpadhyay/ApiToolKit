using ApiToolKit.Application.DTOs.CampaignManager.City;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// cities (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCityController : ControllerBase
{
    private readonly ICampaignManagerCityService _cityService;

    public CampaignManagerCityController(ICampaignManagerCityService cityService)
    {
        _cityService = cityService;
    }

    /// <summary>Retrieves a list of cities, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/cities")]
    public async Task<IActionResult> ListCitiesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? regionDartIds = null, [FromQuery] string? namePrefix = null, [FromQuery] List<string>? dartIds = null, [FromQuery] List<string>? countryDartIds = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCitiesRequest { AccountKey = accountKey, UserprofileId = userprofileId, RegionDartIds = regionDartIds, NamePrefix = namePrefix, DartIds = dartIds, CountryDartIds = countryDartIds };
        var result = await _cityService.ListCitiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.PostalCode;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// postalCodes (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerPostalCodeController : ControllerBase
{
    private readonly ICampaignManagerPostalCodeService _postalCodeService;

    public CampaignManagerPostalCodeController(ICampaignManagerPostalCodeService postalCodeService)
    {
        _postalCodeService = postalCodeService;
    }

    /// <summary>Retrieves a list of postal codes.</summary>
    [HttpGet("userprofiles/{userprofileId}/postalCodes")]
    public async Task<IActionResult> ListPostalCodesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPostalCodesRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _postalCodeService.ListPostalCodesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one postal code by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/postalCodes/{postalCodeId}")]
    public async Task<IActionResult> GetPostalCodeAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string postalCodeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPostalCodeRequest { AccountKey = accountKey, UserprofileId = userprofileId, PostalCodeId = postalCodeId };
        var result = await _postalCodeService.GetPostalCodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

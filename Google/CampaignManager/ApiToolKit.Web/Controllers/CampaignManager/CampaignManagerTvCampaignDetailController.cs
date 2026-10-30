using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignDetail;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// tvCampaignDetails (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerTvCampaignDetailController : ControllerBase
{
    private readonly ICampaignManagerTvCampaignDetailService _tvCampaignDetailService;

    public CampaignManagerTvCampaignDetailController(ICampaignManagerTvCampaignDetailService tvCampaignDetailService)
    {
        _tvCampaignDetailService = tvCampaignDetailService;
    }

    /// <summary>Gets one TvCampaignDetail by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/tvCampaignDetails/{tvCampaignDetailId}")]
    public async Task<IActionResult> GetTvCampaignDetailAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string tvCampaignDetailId, [FromQuery] string? countryDartId = null, [FromQuery] string? accountId = null, [FromQuery] string? tvDataProvider = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTvCampaignDetailRequest { AccountKey = accountKey, UserprofileId = userprofileId, TvCampaignDetailId = tvCampaignDetailId, CountryDartId = countryDartId, AccountId = accountId, TvDataProvider = tvDataProvider };
        var result = await _tvCampaignDetailService.GetTvCampaignDetailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

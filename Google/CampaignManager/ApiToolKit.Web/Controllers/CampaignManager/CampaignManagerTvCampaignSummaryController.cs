using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignSummary;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// tvCampaignSummaries (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerTvCampaignSummaryController : ControllerBase
{
    private readonly ICampaignManagerTvCampaignSummaryService _tvCampaignSummaryService;

    public CampaignManagerTvCampaignSummaryController(ICampaignManagerTvCampaignSummaryService tvCampaignSummaryService)
    {
        _tvCampaignSummaryService = tvCampaignSummaryService;
    }

    /// <summary>Retrieves a list of TV campaign summaries.</summary>
    [HttpGet("userprofiles/{userprofileId}/tvCampaignSummaries")]
    public async Task<IActionResult> ListTvCampaignSummariesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? countryDartId = null, [FromQuery] string? name = null, [FromQuery] string? accountId = null, [FromQuery] string? tvDataProvider = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListTvCampaignSummariesRequest { AccountKey = accountKey, UserprofileId = userprofileId, CountryDartId = countryDartId, Name = name, AccountId = accountId, TvDataProvider = tvDataProvider };
        var result = await _tvCampaignSummaryService.ListTvCampaignSummariesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

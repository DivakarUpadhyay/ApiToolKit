using ApiToolKit.Application.DTOs.CampaignManager.AccountActiveAdSummary;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// accountActiveAdSummaries (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAccountActiveAdSummaryController : ControllerBase
{
    private readonly ICampaignManagerAccountActiveAdSummaryService _accountActiveAdSummaryService;

    public CampaignManagerAccountActiveAdSummaryController(ICampaignManagerAccountActiveAdSummaryService accountActiveAdSummaryService)
    {
        _accountActiveAdSummaryService = accountActiveAdSummaryService;
    }

    /// <summary>Gets the account's active ad summary by account ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountActiveAdSummaries/{accountActiveAdSummaryId}")]
    public async Task<IActionResult> GetAccountActiveAdSummaryAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string accountActiveAdSummaryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountActiveAdSummaryRequest { AccountKey = accountKey, UserprofileId = userprofileId, AccountActiveAdSummaryId = accountActiveAdSummaryId };
        var result = await _accountActiveAdSummaryService.GetAccountActiveAdSummaryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.BillingRate;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// billingRates (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerBillingRateController : ControllerBase
{
    private readonly ICampaignManagerBillingRateService _billingRateService;

    public CampaignManagerBillingRateController(ICampaignManagerBillingRateService billingRateService)
    {
        _billingRateService = billingRateService;
    }

    /// <summary>Retrieves a list of billing rates.</summary>
    [HttpGet("userprofiles/{userprofileId}/billingProfiles/{billingProfileId}/billingRates")]
    public async Task<IActionResult> ListBillingRatesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string billingProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListBillingRatesRequest { AccountKey = accountKey, UserprofileId = userprofileId, BillingProfileId = billingProfileId };
        var result = await _billingRateService.ListBillingRatesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

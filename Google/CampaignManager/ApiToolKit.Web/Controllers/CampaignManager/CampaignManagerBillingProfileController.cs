using ApiToolKit.Application.DTOs.CampaignManager.BillingProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// billingProfiles (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerBillingProfileController : ControllerBase
{
    private readonly ICampaignManagerBillingProfileService _billingProfileService;

    public CampaignManagerBillingProfileController(ICampaignManagerBillingProfileService billingProfileService)
    {
        _billingProfileService = billingProfileService;
    }

    /// <summary>Retrieves a list of billing profiles, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/billingProfiles")]
    public async Task<IActionResult> ListBillingProfilesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? ids = null, [FromQuery] bool? onlySuggestion = null, [FromQuery] List<string>? subaccountIds = null, [FromQuery] string? pageToken = null, [FromQuery] string? currencyCode = null, [FromQuery] List<string>? status = null, [FromQuery] string? sortField = null, [FromQuery] string? name = null, [FromQuery] int? maxResults = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListBillingProfilesRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortOrder = sortOrder, Ids = ids, OnlySuggestion = onlySuggestion, SubaccountIds = subaccountIds, PageToken = pageToken, CurrencyCode = currencyCode, Status = status, SortField = sortField, Name = name, MaxResults = maxResults };
        var result = await _billingProfileService.ListBillingProfilesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one billing profile by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/billingProfiles/{billingProfileId}")]
    public async Task<IActionResult> GetBillingProfileAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string billingProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetBillingProfileRequest { AccountKey = accountKey, UserprofileId = userprofileId, BillingProfileId = billingProfileId };
        var result = await _billingProfileService.GetBillingProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing billing profile.</summary>
    [HttpPut("userprofiles/{userprofileId}/billingProfiles")]
    public async Task<IActionResult> UpdateBillingProfileAsync([FromRoute] string userprofileId, [FromBody] UpdateBillingProfileRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _billingProfileService.UpdateBillingProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

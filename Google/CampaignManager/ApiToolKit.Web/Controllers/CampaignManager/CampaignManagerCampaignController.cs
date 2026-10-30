using ApiToolKit.Application.DTOs.CampaignManager.Campaign;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// campaigns (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCampaignController : ControllerBase
{
    private readonly ICampaignManagerCampaignService _campaignService;

    public CampaignManagerCampaignController(ICampaignManagerCampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    /// <summary>Inserts a new campaign.</summary>
    [HttpPost("userprofiles/{userprofileId}/campaigns")]
    public async Task<IActionResult> InsertCampaignAsync([FromRoute] string userprofileId, [FromBody] InsertCampaignRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.InsertCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of campaigns, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/campaigns")]
    public async Task<IActionResult> ListCampaignsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? pageToken = null, [FromQuery] bool? atLeastOneOptimizationActivity = null, [FromQuery] List<string>? advertiserGroupIds = null, [FromQuery] string? searchString = null, [FromQuery] List<string>? excludedIds = null, [FromQuery] bool? archived = null, [FromQuery] string? subaccountId = null, [FromQuery] List<string>? ids = null, [FromQuery] string? sortOrder = null, [FromQuery] string? sortField = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? advertiserIds = null, [FromQuery] string? overriddenEventTagId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCampaignsRequest { AccountKey = accountKey, UserprofileId = userprofileId, PageToken = pageToken, AtLeastOneOptimizationActivity = atLeastOneOptimizationActivity, AdvertiserGroupIds = advertiserGroupIds, SearchString = searchString, ExcludedIds = excludedIds, Archived = archived, SubaccountId = subaccountId, Ids = ids, SortOrder = sortOrder, SortField = sortField, MaxResults = maxResults, AdvertiserIds = advertiserIds, OverriddenEventTagId = overriddenEventTagId };
        var result = await _campaignService.ListCampaignsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one campaign by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/campaigns/{campaignId}")]
    public async Task<IActionResult> GetCampaignAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string campaignId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCampaignRequest { AccountKey = accountKey, UserprofileId = userprofileId, CampaignId = campaignId };
        var result = await _campaignService.GetCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing campaign.</summary>
    [HttpPut("userprofiles/{userprofileId}/campaigns")]
    public async Task<IActionResult> UpdateCampaignAsync([FromRoute] string userprofileId, [FromBody] UpdateCampaignRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.UpdateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing campaign.</summary>
    [HttpPatch("userprofiles/{userprofileId}/campaigns")]
    public async Task<IActionResult> PatchCampaignAsync([FromRoute] string userprofileId, [FromBody] PatchCampaignRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.PatchCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

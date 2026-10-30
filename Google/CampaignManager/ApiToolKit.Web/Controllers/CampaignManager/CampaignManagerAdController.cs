using ApiToolKit.Application.DTOs.CampaignManager.Ad;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// ads (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAdController : ControllerBase
{
    private readonly ICampaignManagerAdService _adService;

    public CampaignManagerAdController(ICampaignManagerAdService adService)
    {
        _adService = adService;
    }

    /// <summary>Updates an existing ad.</summary>
    [HttpPatch("userprofiles/{userprofileId}/ads")]
    public async Task<IActionResult> UpdateAdAsync([FromRoute] string userprofileId, [FromBody] UpdateAdRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.UpdateAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of ads, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/ads")]
    public async Task<IActionResult> ListAdsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? advertiserId = null, [FromQuery] bool? sslRequired = null, [FromQuery] List<string>? placementIds = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? type = null, [FromQuery] bool? archived = null, [FromQuery] int? maxResults = null, [FromQuery] bool? sslCompliant = null, [FromQuery] List<string>? creativeOptimizationConfigurationIds = null, [FromQuery] List<string>? landingPageIds = null, [FromQuery] List<string>? sizeIds = null, [FromQuery] List<string>? audienceSegmentIds = null, [FromQuery] List<string>? ids = null, [FromQuery] bool? active = null, [FromQuery] string? sortField = null, [FromQuery] string? overriddenEventTagId = null, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? remarketingListIds = null, [FromQuery] List<string>? creativeIds = null, [FromQuery] bool? dynamicClickTracker = null, [FromQuery] List<string>? campaignIds = null, [FromQuery] string? compatibility = null, [FromQuery] string? searchString = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdsRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserId = advertiserId, SslRequired = sslRequired, PlacementIds = placementIds, PageToken = pageToken, Type = type, Archived = archived, MaxResults = maxResults, SslCompliant = sslCompliant, CreativeOptimizationConfigurationIds = creativeOptimizationConfigurationIds, LandingPageIds = landingPageIds, SizeIds = sizeIds, AudienceSegmentIds = audienceSegmentIds, Ids = ids, Active = active, SortField = sortField, OverriddenEventTagId = overriddenEventTagId, SortOrder = sortOrder, RemarketingListIds = remarketingListIds, CreativeIds = creativeIds, DynamicClickTracker = dynamicClickTracker, CampaignIds = campaignIds, Compatibility = compatibility, SearchString = searchString };
        var result = await _adService.ListAdsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one ad by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/ads/{adId}")]
    public async Task<IActionResult> GetAdAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string adId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdId = adId };
        var result = await _adService.GetAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing ad.</summary>
    [HttpPut("userprofiles/{userprofileId}/ads")]
    public async Task<IActionResult> UpdateAdAltAsync([FromRoute] string userprofileId, [FromBody] UpdateAdAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.UpdateAdAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new ad.</summary>
    [HttpPost("userprofiles/{userprofileId}/ads")]
    public async Task<IActionResult> InsertAdAsync([FromRoute] string userprofileId, [FromBody] InsertAdRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.InsertAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

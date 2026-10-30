using ApiToolKit.Application.DTOs.CampaignManager.Site;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// sites (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerSiteController : ControllerBase
{
    private readonly ICampaignManagerSiteService _siteService;

    public CampaignManagerSiteController(ICampaignManagerSiteService siteService)
    {
        _siteService = siteService;
    }

    /// <summary>Retrieves a list of sites, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/sites")]
    public async Task<IActionResult> ListSitesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] bool? approved = null, [FromQuery] List<string>? directorySiteIds = null, [FromQuery] bool? acceptsInterstitialPlacements = null, [FromQuery] List<string>? campaignIds = null, [FromQuery] bool? unmappedSite = null, [FromQuery] string? pageToken = null, [FromQuery] string? subaccountId = null, [FromQuery] int? maxResults = null, [FromQuery] bool? acceptsPublisherPaidPlacements = null, [FromQuery] string? searchString = null, [FromQuery] bool? adWordsSite = null, [FromQuery] List<string>? ids = null, [FromQuery] bool? acceptsInStreamVideoPlacements = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSitesRequest { AccountKey = accountKey, UserprofileId = userprofileId, Approved = approved, DirectorySiteIds = directorySiteIds, AcceptsInterstitialPlacements = acceptsInterstitialPlacements, CampaignIds = campaignIds, UnmappedSite = unmappedSite, PageToken = pageToken, SubaccountId = subaccountId, MaxResults = maxResults, AcceptsPublisherPaidPlacements = acceptsPublisherPaidPlacements, SearchString = searchString, AdWordsSite = adWordsSite, Ids = ids, AcceptsInStreamVideoPlacements = acceptsInStreamVideoPlacements, SortField = sortField, SortOrder = sortOrder };
        var result = await _siteService.ListSitesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new site.</summary>
    [HttpPost("userprofiles/{userprofileId}/sites")]
    public async Task<IActionResult> InsertSiteAsync([FromRoute] string userprofileId, [FromBody] InsertSiteRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _siteService.InsertSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing site.</summary>
    [HttpPatch("userprofiles/{userprofileId}/sites")]
    public async Task<IActionResult> UpdateSiteAsync([FromRoute] string userprofileId, [FromBody] UpdateSiteRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _siteService.UpdateSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one site by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/sites/{siteId}")]
    public async Task<IActionResult> GetSiteAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string siteId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetSiteRequest { AccountKey = accountKey, UserprofileId = userprofileId, SiteId = siteId };
        var result = await _siteService.GetSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing site.</summary>
    [HttpPut("userprofiles/{userprofileId}/sites")]
    public async Task<IActionResult> UpdateSiteAltAsync([FromRoute] string userprofileId, [FromBody] UpdateSiteAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _siteService.UpdateSiteAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

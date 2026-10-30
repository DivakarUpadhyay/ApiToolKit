using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserLandingPage;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// advertiserLandingPages (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAdvertiserLandingPageController : ControllerBase
{
    private readonly ICampaignManagerAdvertiserLandingPageService _advertiserLandingPageService;

    public CampaignManagerAdvertiserLandingPageController(ICampaignManagerAdvertiserLandingPageService advertiserLandingPageService)
    {
        _advertiserLandingPageService = advertiserLandingPageService;
    }

    /// <summary>Gets one landing page by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertiserLandingPages/{advertiserLandingPageId}")]
    public async Task<IActionResult> GetAdvertiserLandingPageAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string advertiserLandingPageId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdvertiserLandingPageRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserLandingPageId = advertiserLandingPageId };
        var result = await _advertiserLandingPageService.GetAdvertiserLandingPageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing landing page.</summary>
    [HttpPatch("userprofiles/{userprofileId}/advertiserLandingPages")]
    public async Task<IActionResult> UpdateAdvertiserLandingPageAsync([FromRoute] string userprofileId, [FromBody] UpdateAdvertiserLandingPageRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserLandingPageService.UpdateAdvertiserLandingPageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new landing page.</summary>
    [HttpPost("userprofiles/{userprofileId}/advertiserLandingPages")]
    public async Task<IActionResult> InsertAdvertiserLandingPageAsync([FromRoute] string userprofileId, [FromBody] InsertAdvertiserLandingPageRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserLandingPageService.InsertAdvertiserLandingPageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing landing page.</summary>
    [HttpPut("userprofiles/{userprofileId}/advertiserLandingPages")]
    public async Task<IActionResult> UpdateAdvertiserLandingPageAltAsync([FromRoute] string userprofileId, [FromBody] UpdateAdvertiserLandingPageAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserLandingPageService.UpdateAdvertiserLandingPageAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of landing pages.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertiserLandingPages")]
    public async Task<IActionResult> ListAdvertiserLandingPagesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? searchString = null, [FromQuery] List<string>? campaignIds = null, [FromQuery] int? maxResults = null, [FromQuery] string? subaccountId = null, [FromQuery] bool? archived = null, [FromQuery] List<string>? ids = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? advertiserIds = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdvertiserLandingPagesRequest { AccountKey = accountKey, UserprofileId = userprofileId, SearchString = searchString, CampaignIds = campaignIds, MaxResults = maxResults, SubaccountId = subaccountId, Archived = archived, Ids = ids, SortField = sortField, SortOrder = sortOrder, AdvertiserIds = advertiserIds, PageToken = pageToken };
        var result = await _advertiserLandingPageService.ListAdvertiserLandingPagesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

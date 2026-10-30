using ApiToolKit.Application.DTOs.CampaignManager.DirectorySite;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// directorySites (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerDirectorySiteController : ControllerBase
{
    private readonly ICampaignManagerDirectorySiteService _directorySiteService;

    public CampaignManagerDirectorySiteController(ICampaignManagerDirectorySiteService directorySiteService)
    {
        _directorySiteService = directorySiteService;
    }

    /// <summary>Retrieves a list of directory sites, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/directorySites")]
    public async Task<IActionResult> ListDirectorySitesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortField = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] string? sortOrder = null, [FromQuery] bool? acceptsInStreamVideoPlacements = null, [FromQuery] bool? active = null, [FromQuery] List<string>? ids = null, [FromQuery] string? dfpNetworkCode = null, [FromQuery] bool? acceptsPublisherPaidPlacements = null, [FromQuery] bool? acceptsInterstitialPlacements = null, [FromQuery] int? maxResults = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListDirectorySitesRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortField = sortField, SearchString = searchString, PageToken = pageToken, SortOrder = sortOrder, AcceptsInStreamVideoPlacements = acceptsInStreamVideoPlacements, Active = active, Ids = ids, DfpNetworkCode = dfpNetworkCode, AcceptsPublisherPaidPlacements = acceptsPublisherPaidPlacements, AcceptsInterstitialPlacements = acceptsInterstitialPlacements, MaxResults = maxResults };
        var result = await _directorySiteService.ListDirectorySitesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new directory site.</summary>
    [HttpPost("userprofiles/{userprofileId}/directorySites")]
    public async Task<IActionResult> InsertDirectorySiteAsync([FromRoute] string userprofileId, [FromBody] InsertDirectorySiteRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _directorySiteService.InsertDirectorySiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one directory site by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/directorySites/{directorySiteId}")]
    public async Task<IActionResult> GetDirectorySiteAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string directorySiteId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetDirectorySiteRequest { AccountKey = accountKey, UserprofileId = userprofileId, DirectorySiteId = directorySiteId };
        var result = await _directorySiteService.GetDirectorySiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

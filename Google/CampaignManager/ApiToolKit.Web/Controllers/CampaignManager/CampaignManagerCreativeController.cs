using ApiToolKit.Application.DTOs.CampaignManager.Creative;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// creatives (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCreativeController : ControllerBase
{
    private readonly ICampaignManagerCreativeService _creativeService;

    public CampaignManagerCreativeController(ICampaignManagerCreativeService creativeService)
    {
        _creativeService = creativeService;
    }

    /// <summary>Retrieves a list of creatives, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/creatives")]
    public async Task<IActionResult> ListCreativesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? companionCreativeIds = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] bool? archived = null, [FromQuery] string? studioCreativeId = null, [FromQuery] string? campaignId = null, [FromQuery] List<string>? sizeIds = null, [FromQuery] string? advertiserId = null, [FromQuery] List<string>? ids = null, [FromQuery] bool? active = null, [FromQuery] string? searchString = null, [FromQuery] int? maxResults = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? creativeFieldIds = null, [FromQuery] List<string>? types = null, [FromQuery] List<string>? renderingIds = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCreativesRequest { AccountKey = accountKey, UserprofileId = userprofileId, CompanionCreativeIds = companionCreativeIds, SortField = sortField, SortOrder = sortOrder, Archived = archived, StudioCreativeId = studioCreativeId, CampaignId = campaignId, SizeIds = sizeIds, AdvertiserId = advertiserId, Ids = ids, Active = active, SearchString = searchString, MaxResults = maxResults, PageToken = pageToken, CreativeFieldIds = creativeFieldIds, Types = types, RenderingIds = renderingIds };
        var result = await _creativeService.ListCreativesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative.</summary>
    [HttpPatch("userprofiles/{userprofileId}/creatives")]
    public async Task<IActionResult> UpdateCreativeAsync([FromRoute] string userprofileId, [FromBody] UpdateCreativeRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeService.UpdateCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative.</summary>
    [HttpPut("userprofiles/{userprofileId}/creatives")]
    public async Task<IActionResult> UpdateCreativeAltAsync([FromRoute] string userprofileId, [FromBody] UpdateCreativeAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeService.UpdateCreativeAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one creative by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/creatives/{creativeId}")]
    public async Task<IActionResult> GetCreativeAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCreativeRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeId = creativeId };
        var result = await _creativeService.GetCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new creative.</summary>
    [HttpPost("userprofiles/{userprofileId}/creatives")]
    public async Task<IActionResult> InsertCreativeAsync([FromRoute] string userprofileId, [FromBody] InsertCreativeRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeService.InsertCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

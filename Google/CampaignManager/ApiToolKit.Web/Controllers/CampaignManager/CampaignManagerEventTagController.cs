using ApiToolKit.Application.DTOs.CampaignManager.EventTag;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// eventTags (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerEventTagController : ControllerBase
{
    private readonly ICampaignManagerEventTagService _eventTagService;

    public CampaignManagerEventTagController(ICampaignManagerEventTagService eventTagService)
    {
        _eventTagService = eventTagService;
    }

    /// <summary>Retrieves a list of event tags, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/eventTags")]
    public async Task<IActionResult> ListEventTagsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? ids = null, [FromQuery] string? sortOrder = null, [FromQuery] string? advertiserId = null, [FromQuery] List<string>? eventTagTypes = null, [FromQuery] bool? enabled = null, [FromQuery] string? sortField = null, [FromQuery] string? campaignId = null, [FromQuery] string? adId = null, [FromQuery] string? searchString = null, [FromQuery] bool? definitionsOnly = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListEventTagsRequest { AccountKey = accountKey, UserprofileId = userprofileId, Ids = ids, SortOrder = sortOrder, AdvertiserId = advertiserId, EventTagTypes = eventTagTypes, Enabled = enabled, SortField = sortField, CampaignId = campaignId, AdId = adId, SearchString = searchString, DefinitionsOnly = definitionsOnly };
        var result = await _eventTagService.ListEventTagsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing event tag.</summary>
    [HttpPut("userprofiles/{userprofileId}/eventTags")]
    public async Task<IActionResult> UpdateEventTagAsync([FromRoute] string userprofileId, [FromBody] UpdateEventTagRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventTagService.UpdateEventTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing event tag.</summary>
    [HttpPatch("userprofiles/{userprofileId}/eventTags")]
    public async Task<IActionResult> PatchEventTagAsync([FromRoute] string userprofileId, [FromBody] PatchEventTagRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventTagService.PatchEventTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing event tag.</summary>
    [HttpDelete("userprofiles/{userprofileId}/eventTags/{eventTagId}")]
    public async Task<IActionResult> DeleteEventTagAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string eventTagId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteEventTagRequest { AccountKey = accountKey, UserprofileId = userprofileId, EventTagId = eventTagId };
        var result = await _eventTagService.DeleteEventTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one event tag by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/eventTags/{eventTagId}")]
    public async Task<IActionResult> GetEventTagAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string eventTagId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetEventTagRequest { AccountKey = accountKey, UserprofileId = userprofileId, EventTagId = eventTagId };
        var result = await _eventTagService.GetEventTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new event tag.</summary>
    [HttpPost("userprofiles/{userprofileId}/eventTags")]
    public async Task<IActionResult> InsertEventTagAsync([FromRoute] string userprofileId, [FromBody] InsertEventTagRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventTagService.InsertEventTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

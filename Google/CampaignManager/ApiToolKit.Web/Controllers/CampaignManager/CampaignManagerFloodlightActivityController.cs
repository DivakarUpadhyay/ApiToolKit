using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivity;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// floodlightActivities (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerFloodlightActivityController : ControllerBase
{
    private readonly ICampaignManagerFloodlightActivityService _floodlightActivityService;

    public CampaignManagerFloodlightActivityController(ICampaignManagerFloodlightActivityService floodlightActivityService)
    {
        _floodlightActivityService = floodlightActivityService;
    }

    /// <summary>Updates an existing floodlight activity.</summary>
    [HttpPatch("userprofiles/{userprofileId}/floodlightActivities")]
    public async Task<IActionResult> UpdateFloodlightActivityAsync([FromRoute] string userprofileId, [FromBody] UpdateFloodlightActivityRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityService.UpdateFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing floodlight activity.</summary>
    [HttpPut("userprofiles/{userprofileId}/floodlightActivities")]
    public async Task<IActionResult> UpdateFloodlightActivityAltAsync([FromRoute] string userprofileId, [FromBody] UpdateFloodlightActivityAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityService.UpdateFloodlightActivityAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing floodlight activity.</summary>
    [HttpDelete("userprofiles/{userprofileId}/floodlightActivities/{floodlightActivityId}")]
    public async Task<IActionResult> DeleteFloodlightActivityAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string floodlightActivityId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteFloodlightActivityRequest { AccountKey = accountKey, UserprofileId = userprofileId, FloodlightActivityId = floodlightActivityId };
        var result = await _floodlightActivityService.DeleteFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of floodlight activities, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightActivities")]
    public async Task<IActionResult> ListFloodlightActivitiesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? ids = null, [FromQuery] string? sortOrder = null, [FromQuery] string? tagString = null, [FromQuery] string? sortField = null, [FromQuery] int? maxResults = null, [FromQuery] string? searchString = null, [FromQuery] List<string>? floodlightActivityGroupIds = null, [FromQuery] string? floodlightActivityGroupTagString = null, [FromQuery] string? floodlightActivityGroupName = null, [FromQuery] string? pageToken = null, [FromQuery] string? floodlightConfigurationId = null, [FromQuery] string? advertiserId = null, [FromQuery] string? floodlightActivityGroupType = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFloodlightActivitiesRequest { AccountKey = accountKey, UserprofileId = userprofileId, Ids = ids, SortOrder = sortOrder, TagString = tagString, SortField = sortField, MaxResults = maxResults, SearchString = searchString, FloodlightActivityGroupIds = floodlightActivityGroupIds, FloodlightActivityGroupTagString = floodlightActivityGroupTagString, FloodlightActivityGroupName = floodlightActivityGroupName, PageToken = pageToken, FloodlightConfigurationId = floodlightConfigurationId, AdvertiserId = advertiserId, FloodlightActivityGroupType = floodlightActivityGroupType };
        var result = await _floodlightActivityService.ListFloodlightActivitiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one floodlight activity by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightActivities/{floodlightActivityId}")]
    public async Task<IActionResult> GetFloodlightActivityAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string floodlightActivityId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFloodlightActivityRequest { AccountKey = accountKey, UserprofileId = userprofileId, FloodlightActivityId = floodlightActivityId };
        var result = await _floodlightActivityService.GetFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates a tag for a floodlight activity.</summary>
    [HttpPost("userprofiles/{userprofileId}/floodlightActivities/generatetag")]
    public async Task<IActionResult> GeneratetagFloodlightActivityAsync([FromRoute] string userprofileId, [FromBody] GeneratetagFloodlightActivityRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityService.GeneratetagFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new floodlight activity.</summary>
    [HttpPost("userprofiles/{userprofileId}/floodlightActivities")]
    public async Task<IActionResult> InsertFloodlightActivityAsync([FromRoute] string userprofileId, [FromBody] InsertFloodlightActivityRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityService.InsertFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

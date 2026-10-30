using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivityGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// floodlightActivityGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerFloodlightActivityGroupController : ControllerBase
{
    private readonly ICampaignManagerFloodlightActivityGroupService _floodlightActivityGroupService;

    public CampaignManagerFloodlightActivityGroupController(ICampaignManagerFloodlightActivityGroupService floodlightActivityGroupService)
    {
        _floodlightActivityGroupService = floodlightActivityGroupService;
    }

    /// <summary>Retrieves a list of floodlight activity groups, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightActivityGroups")]
    public async Task<IActionResult> ListFloodlightActivityGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] int? maxResults = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] string? type = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? ids = null, [FromQuery] string? advertiserId = null, [FromQuery] string? floodlightConfigurationId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFloodlightActivityGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId, MaxResults = maxResults, SortField = sortField, SortOrder = sortOrder, Type = type, SearchString = searchString, PageToken = pageToken, Ids = ids, AdvertiserId = advertiserId, FloodlightConfigurationId = floodlightConfigurationId };
        var result = await _floodlightActivityGroupService.ListFloodlightActivityGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing floodlight activity group.</summary>
    [HttpPatch("userprofiles/{userprofileId}/floodlightActivityGroups")]
    public async Task<IActionResult> UpdateFloodlightActivityGroupAsync([FromRoute] string userprofileId, [FromBody] UpdateFloodlightActivityGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityGroupService.UpdateFloodlightActivityGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new floodlight activity group.</summary>
    [HttpPost("userprofiles/{userprofileId}/floodlightActivityGroups")]
    public async Task<IActionResult> InsertFloodlightActivityGroupAsync([FromRoute] string userprofileId, [FromBody] InsertFloodlightActivityGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityGroupService.InsertFloodlightActivityGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing floodlight activity group.</summary>
    [HttpPut("userprofiles/{userprofileId}/floodlightActivityGroups")]
    public async Task<IActionResult> UpdateFloodlightActivityGroupAltAsync([FromRoute] string userprofileId, [FromBody] UpdateFloodlightActivityGroupAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightActivityGroupService.UpdateFloodlightActivityGroupAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one floodlight activity group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/floodlightActivityGroups/{floodlightActivityGroupId}")]
    public async Task<IActionResult> GetFloodlightActivityGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string floodlightActivityGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFloodlightActivityGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, FloodlightActivityGroupId = floodlightActivityGroupId };
        var result = await _floodlightActivityGroupService.GetFloodlightActivityGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

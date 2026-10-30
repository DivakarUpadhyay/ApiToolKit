using ApiToolKit.Application.DTOs.CampaignManager.CreativeGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// creativeGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCreativeGroupController : ControllerBase
{
    private readonly ICampaignManagerCreativeGroupService _creativeGroupService;

    public CampaignManagerCreativeGroupController(ICampaignManagerCreativeGroupService creativeGroupService)
    {
        _creativeGroupService = creativeGroupService;
    }

    /// <summary>Gets one creative group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeGroups/{creativeGroupId}")]
    public async Task<IActionResult> GetCreativeGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCreativeGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeGroupId = creativeGroupId };
        var result = await _creativeGroupService.GetCreativeGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative group.</summary>
    [HttpPut("userprofiles/{userprofileId}/creativeGroups")]
    public async Task<IActionResult> UpdateCreativeGroupAsync([FromRoute] string userprofileId, [FromBody] UpdateCreativeGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeGroupService.UpdateCreativeGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative group.</summary>
    [HttpPatch("userprofiles/{userprofileId}/creativeGroups")]
    public async Task<IActionResult> PatchCreativeGroupAsync([FromRoute] string userprofileId, [FromBody] PatchCreativeGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeGroupService.PatchCreativeGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of creative groups, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeGroups")]
    public async Task<IActionResult> ListCreativeGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? searchString = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] int? groupNumber = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? ids = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? advertiserIds = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCreativeGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SearchString = searchString, SortField = sortField, SortOrder = sortOrder, GroupNumber = groupNumber, PageToken = pageToken, Ids = ids, MaxResults = maxResults, AdvertiserIds = advertiserIds };
        var result = await _creativeGroupService.ListCreativeGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new creative group.</summary>
    [HttpPost("userprofiles/{userprofileId}/creativeGroups")]
    public async Task<IActionResult> InsertCreativeGroupAsync([FromRoute] string userprofileId, [FromBody] InsertCreativeGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeGroupService.InsertCreativeGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// advertiserGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAdvertiserGroupController : ControllerBase
{
    private readonly ICampaignManagerAdvertiserGroupService _advertiserGroupService;

    public CampaignManagerAdvertiserGroupController(ICampaignManagerAdvertiserGroupService advertiserGroupService)
    {
        _advertiserGroupService = advertiserGroupService;
    }

    /// <summary>Deletes an existing advertiser group.</summary>
    [HttpDelete("userprofiles/{userprofileId}/advertiserGroups/{advertiserGroupId}")]
    public async Task<IActionResult> DeleteAdvertiserGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string advertiserGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAdvertiserGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserGroupId = advertiserGroupId };
        var result = await _advertiserGroupService.DeleteAdvertiserGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of advertiser groups, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertiserGroups")]
    public async Task<IActionResult> ListAdvertiserGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortField = null, [FromQuery] string? searchString = null, [FromQuery] string? sortOrder = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? ids = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdvertiserGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortField = sortField, SearchString = searchString, SortOrder = sortOrder, MaxResults = maxResults, Ids = ids, PageToken = pageToken };
        var result = await _advertiserGroupService.ListAdvertiserGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing advertiser group.</summary>
    [HttpPut("userprofiles/{userprofileId}/advertiserGroups")]
    public async Task<IActionResult> UpdateAdvertiserGroupAsync([FromRoute] string userprofileId, [FromBody] UpdateAdvertiserGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserGroupService.UpdateAdvertiserGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new advertiser group.</summary>
    [HttpPost("userprofiles/{userprofileId}/advertiserGroups")]
    public async Task<IActionResult> InsertAdvertiserGroupAsync([FromRoute] string userprofileId, [FromBody] InsertAdvertiserGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserGroupService.InsertAdvertiserGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one advertiser group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertiserGroups/{advertiserGroupId}")]
    public async Task<IActionResult> GetAdvertiserGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string advertiserGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdvertiserGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserGroupId = advertiserGroupId };
        var result = await _advertiserGroupService.GetAdvertiserGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing advertiser group.</summary>
    [HttpPatch("userprofiles/{userprofileId}/advertiserGroups")]
    public async Task<IActionResult> PatchAdvertiserGroupAsync([FromRoute] string userprofileId, [FromBody] PatchAdvertiserGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserGroupService.PatchAdvertiserGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.PlacementGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// placementGroups (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerPlacementGroupController : ControllerBase
{
    private readonly ICampaignManagerPlacementGroupService _placementGroupService;

    public CampaignManagerPlacementGroupController(ICampaignManagerPlacementGroupService placementGroupService)
    {
        _placementGroupService = placementGroupService;
    }

    /// <summary>Updates an existing placement group.</summary>
    [HttpPut("userprofiles/{userprofileId}/placementGroups")]
    public async Task<IActionResult> UpdatePlacementGroupAsync([FromRoute] string userprofileId, [FromBody] UpdatePlacementGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementGroupService.UpdatePlacementGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one placement group by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/placementGroups/{placementGroupId}")]
    public async Task<IActionResult> GetPlacementGroupAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string placementGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPlacementGroupRequest { AccountKey = accountKey, UserprofileId = userprofileId, PlacementGroupId = placementGroupId };
        var result = await _placementGroupService.GetPlacementGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing placement group.</summary>
    [HttpPatch("userprofiles/{userprofileId}/placementGroups")]
    public async Task<IActionResult> PatchPlacementGroupAsync([FromRoute] string userprofileId, [FromBody] PatchPlacementGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementGroupService.PatchPlacementGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new placement group.</summary>
    [HttpPost("userprofiles/{userprofileId}/placementGroups")]
    public async Task<IActionResult> InsertPlacementGroupAsync([FromRoute] string userprofileId, [FromBody] InsertPlacementGroupRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementGroupService.InsertPlacementGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of placement groups, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/placementGroups")]
    public async Task<IActionResult> ListPlacementGroupsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortField = null, [FromQuery] List<string>? ids = null, [FromQuery] List<string>? contentCategoryIds = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? activeStatus = null, [FromQuery] List<string>? siteIds = null, [FromQuery] List<string>? pricingTypes = null, [FromQuery] List<string>? placementStrategyIds = null, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? directorySiteIds = null, [FromQuery] string? minEndDate = null, [FromQuery] string? minStartDate = null, [FromQuery] string? placementGroupType = null, [FromQuery] List<string>? campaignIds = null, [FromQuery] List<string>? advertiserIds = null, [FromQuery] string? pageToken = null, [FromQuery] string? searchString = null, [FromQuery] string? maxStartDate = null, [FromQuery] string? maxEndDate = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPlacementGroupsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortField = sortField, Ids = ids, ContentCategoryIds = contentCategoryIds, MaxResults = maxResults, ActiveStatus = activeStatus, SiteIds = siteIds, PricingTypes = pricingTypes, PlacementStrategyIds = placementStrategyIds, SortOrder = sortOrder, DirectorySiteIds = directorySiteIds, MinEndDate = minEndDate, MinStartDate = minStartDate, PlacementGroupType = placementGroupType, CampaignIds = campaignIds, AdvertiserIds = advertiserIds, PageToken = pageToken, SearchString = searchString, MaxStartDate = maxStartDate, MaxEndDate = maxEndDate };
        var result = await _placementGroupService.ListPlacementGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

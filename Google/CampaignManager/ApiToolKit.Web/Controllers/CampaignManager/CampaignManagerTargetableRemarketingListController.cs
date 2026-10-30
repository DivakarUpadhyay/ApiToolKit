using ApiToolKit.Application.DTOs.CampaignManager.TargetableRemarketingList;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// targetableRemarketingLists (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerTargetableRemarketingListController : ControllerBase
{
    private readonly ICampaignManagerTargetableRemarketingListService _targetableRemarketingListService;

    public CampaignManagerTargetableRemarketingListController(ICampaignManagerTargetableRemarketingListService targetableRemarketingListService)
    {
        _targetableRemarketingListService = targetableRemarketingListService;
    }

    /// <summary>Retrieves a list of targetable remarketing lists, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/targetableRemarketingLists")]
    public async Task<IActionResult> ListTargetableRemarketingListsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string advertiserId, [FromQuery] string? pageToken = null, [FromQuery] string? sortOrder = null, [FromQuery] string? name = null, [FromQuery] bool? active = null, [FromQuery] string? sortField = null, [FromQuery] int? maxResults = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(advertiserId)) return BadRequest("advertiserId is required.");
        var request = new ListTargetableRemarketingListsRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserId = advertiserId, PageToken = pageToken, SortOrder = sortOrder, Name = name, Active = active, SortField = sortField, MaxResults = maxResults };
        var result = await _targetableRemarketingListService.ListTargetableRemarketingListsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one remarketing list by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/targetableRemarketingLists/{targetableRemarketingListId}")]
    public async Task<IActionResult> GetTargetableRemarketingListAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string targetableRemarketingListId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTargetableRemarketingListRequest { AccountKey = accountKey, UserprofileId = userprofileId, TargetableRemarketingListId = targetableRemarketingListId };
        var result = await _targetableRemarketingListService.GetTargetableRemarketingListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.CampaignManager.RemarketingList;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// remarketingLists (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerRemarketingListController : ControllerBase
{
    private readonly ICampaignManagerRemarketingListService _remarketingListService;

    public CampaignManagerRemarketingListController(ICampaignManagerRemarketingListService remarketingListService)
    {
        _remarketingListService = remarketingListService;
    }

    /// <summary>Retrieves a list of remarketing lists, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/remarketingLists")]
    public async Task<IActionResult> ListRemarketingListsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string advertiserId, [FromQuery] string? sortField = null, [FromQuery] string? pageToken = null, [FromQuery] bool? active = null, [FromQuery] string? sortOrder = null, [FromQuery] string? floodlightActivityId = null, [FromQuery] int? maxResults = null, [FromQuery] string? name = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(advertiserId)) return BadRequest("advertiserId is required.");
        var request = new ListRemarketingListsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortField = sortField, PageToken = pageToken, Active = active, SortOrder = sortOrder, AdvertiserId = advertiserId, FloodlightActivityId = floodlightActivityId, MaxResults = maxResults, Name = name };
        var result = await _remarketingListService.ListRemarketingListsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one remarketing list by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/remarketingLists/{remarketingListId}")]
    public async Task<IActionResult> GetRemarketingListAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string remarketingListId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetRemarketingListRequest { AccountKey = accountKey, UserprofileId = userprofileId, RemarketingListId = remarketingListId };
        var result = await _remarketingListService.GetRemarketingListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing remarketing list.</summary>
    [HttpPatch("userprofiles/{userprofileId}/remarketingLists")]
    public async Task<IActionResult> UpdateRemarketingListAsync([FromRoute] string userprofileId, [FromBody] UpdateRemarketingListRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _remarketingListService.UpdateRemarketingListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing remarketing list.</summary>
    [HttpPut("userprofiles/{userprofileId}/remarketingLists")]
    public async Task<IActionResult> UpdateRemarketingListAltAsync([FromRoute] string userprofileId, [FromBody] UpdateRemarketingListAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _remarketingListService.UpdateRemarketingListAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new remarketing list.</summary>
    [HttpPost("userprofiles/{userprofileId}/remarketingLists")]
    public async Task<IActionResult> InsertRemarketingListAsync([FromRoute] string userprofileId, [FromBody] InsertRemarketingListRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _remarketingListService.InsertRemarketingListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

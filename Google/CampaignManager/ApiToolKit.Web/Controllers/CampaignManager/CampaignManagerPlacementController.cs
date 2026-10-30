using ApiToolKit.Application.DTOs.CampaignManager.Placement;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// placements (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerPlacementController : ControllerBase
{
    private readonly ICampaignManagerPlacementService _placementService;

    public CampaignManagerPlacementController(ICampaignManagerPlacementService placementService)
    {
        _placementService = placementService;
    }

    /// <summary>Updates an existing placement.</summary>
    [HttpPatch("userprofiles/{userprofileId}/placements")]
    public async Task<IActionResult> UpdatePlacementAsync([FromRoute] string userprofileId, [FromBody] UpdatePlacementRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementService.UpdatePlacementAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one placement by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/placements/{placementId}")]
    public async Task<IActionResult> GetPlacementAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string placementId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPlacementRequest { AccountKey = accountKey, UserprofileId = userprofileId, PlacementId = placementId };
        var result = await _placementService.GetPlacementAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new placement.</summary>
    [HttpPost("userprofiles/{userprofileId}/placements")]
    public async Task<IActionResult> InsertPlacementAsync([FromRoute] string userprofileId, [FromBody] InsertPlacementRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementService.InsertPlacementAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates tags for a placement.</summary>
    [HttpPost("userprofiles/{userprofileId}/placements/generatetags")]
    public async Task<IActionResult> GeneratetagsPlacementAsync([FromRoute] string userprofileId, [FromBody] GeneratetagsPlacementRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementService.GeneratetagsPlacementAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing placement.</summary>
    [HttpPut("userprofiles/{userprofileId}/placements")]
    public async Task<IActionResult> UpdatePlacementAltAsync([FromRoute] string userprofileId, [FromBody] UpdatePlacementAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementService.UpdatePlacementAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of placements, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/placements")]
    public async Task<IActionResult> ListPlacementsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? campaignIds = null, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? ids = null, [FromQuery] string? searchString = null, [FromQuery] string? maxStartDate = null, [FromQuery] List<string>? advertiserIds = null, [FromQuery] List<string>? contentCategoryIds = null, [FromQuery] string? minEndDate = null, [FromQuery] List<string>? compatibilities = null, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? sizeIds = null, [FromQuery] List<string>? pricingTypes = null, [FromQuery] List<string>? placementStrategyIds = null, [FromQuery] List<string>? activeStatus = null, [FromQuery] string? sortField = null, [FromQuery] List<string>? siteIds = null, [FromQuery] string? minStartDate = null, [FromQuery] string? maxEndDate = null, [FromQuery] List<string>? groupIds = null, [FromQuery] string? paymentSource = null, [FromQuery] List<string>? directorySiteIds = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPlacementsRequest { AccountKey = accountKey, UserprofileId = userprofileId, CampaignIds = campaignIds, PageToken = pageToken, MaxResults = maxResults, Ids = ids, SearchString = searchString, MaxStartDate = maxStartDate, AdvertiserIds = advertiserIds, ContentCategoryIds = contentCategoryIds, MinEndDate = minEndDate, Compatibilities = compatibilities, SortOrder = sortOrder, SizeIds = sizeIds, PricingTypes = pricingTypes, PlacementStrategyIds = placementStrategyIds, ActiveStatus = activeStatus, SortField = sortField, SiteIds = siteIds, MinStartDate = minStartDate, MaxEndDate = maxEndDate, GroupIds = groupIds, PaymentSource = paymentSource, DirectorySiteIds = directorySiteIds };
        var result = await _placementService.ListPlacementsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

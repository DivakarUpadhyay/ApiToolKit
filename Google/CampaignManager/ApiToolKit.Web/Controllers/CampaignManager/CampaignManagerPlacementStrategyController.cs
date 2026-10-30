using ApiToolKit.Application.DTOs.CampaignManager.PlacementStrategy;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// placementStrategies (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerPlacementStrategyController : ControllerBase
{
    private readonly ICampaignManagerPlacementStrategyService _placementStrategyService;

    public CampaignManagerPlacementStrategyController(ICampaignManagerPlacementStrategyService placementStrategyService)
    {
        _placementStrategyService = placementStrategyService;
    }

    /// <summary>Retrieves a list of placement strategies, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/placementStrategies")]
    public async Task<IActionResult> ListPlacementStrategiesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortField = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? ids = null, [FromQuery] string? pageToken = null, [FromQuery] string? searchString = null, [FromQuery] string? sortOrder = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPlacementStrategiesRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortField = sortField, MaxResults = maxResults, Ids = ids, PageToken = pageToken, SearchString = searchString, SortOrder = sortOrder };
        var result = await _placementStrategyService.ListPlacementStrategiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new placement strategy.</summary>
    [HttpPost("userprofiles/{userprofileId}/placementStrategies")]
    public async Task<IActionResult> InsertPlacementStrategyAsync([FromRoute] string userprofileId, [FromBody] InsertPlacementStrategyRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementStrategyService.InsertPlacementStrategyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing placement strategy.</summary>
    [HttpPatch("userprofiles/{userprofileId}/placementStrategies")]
    public async Task<IActionResult> UpdatePlacementStrategyAsync([FromRoute] string userprofileId, [FromBody] UpdatePlacementStrategyRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementStrategyService.UpdatePlacementStrategyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing placement strategy.</summary>
    [HttpDelete("userprofiles/{userprofileId}/placementStrategies/{placementStrategyId}")]
    public async Task<IActionResult> DeletePlacementStrategyAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string placementStrategyId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeletePlacementStrategyRequest { AccountKey = accountKey, UserprofileId = userprofileId, PlacementStrategyId = placementStrategyId };
        var result = await _placementStrategyService.DeletePlacementStrategyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one placement strategy by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/placementStrategies/{placementStrategyId}")]
    public async Task<IActionResult> GetPlacementStrategyAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string placementStrategyId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPlacementStrategyRequest { AccountKey = accountKey, UserprofileId = userprofileId, PlacementStrategyId = placementStrategyId };
        var result = await _placementStrategyService.GetPlacementStrategyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing placement strategy.</summary>
    [HttpPut("userprofiles/{userprofileId}/placementStrategies")]
    public async Task<IActionResult> UpdatePlacementStrategyAltAsync([FromRoute] string userprofileId, [FromBody] UpdatePlacementStrategyAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placementStrategyService.UpdatePlacementStrategyAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

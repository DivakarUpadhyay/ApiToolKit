using ApiToolKit.Application.DTOs.CampaignManager.DynamicFeed;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// dynamicFeeds (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerDynamicFeedController : ControllerBase
{
    private readonly ICampaignManagerDynamicFeedService _dynamicFeedService;

    public CampaignManagerDynamicFeedController(ICampaignManagerDynamicFeedService dynamicFeedService)
    {
        _dynamicFeedService = dynamicFeedService;
    }

    /// <summary>Gets a dynamic feed by ID.</summary>
    [HttpGet("studio/dynamicFeeds/{dynamicFeedId}")]
    public async Task<IActionResult> GetDynamicFeedAsync([FromQuery] string accountKey, [FromRoute] string dynamicFeedId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetDynamicFeedRequest { AccountKey = accountKey, DynamicFeedId = dynamicFeedId };
        var result = await _dynamicFeedService.GetDynamicFeedAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retransforms a dynamic feed.</summary>
    [HttpPost("studio/dynamicFeeds/{dynamicFeedId}/retransform")]
    public async Task<IActionResult> RetransformDynamicFeedAsync([FromRoute] string dynamicFeedId, [FromBody] RetransformDynamicFeedRequest request)
    {
        request.DynamicFeedId = dynamicFeedId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicFeedService.RetransformDynamicFeedAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new dynamic feed.</summary>
    [HttpPost("studio/dynamicFeeds")]
    public async Task<IActionResult> InsertDynamicFeedAsync([FromBody] InsertDynamicFeedRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicFeedService.InsertDynamicFeedAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a new dynamic feed.</summary>
    [HttpPut("studio/dynamicFeeds")]
    public async Task<IActionResult> UpdateDynamicFeedAsync([FromBody] UpdateDynamicFeedRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicFeedService.UpdateDynamicFeedAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

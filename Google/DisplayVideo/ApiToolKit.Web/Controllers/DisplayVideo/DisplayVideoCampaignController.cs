using ApiToolKit.Application.DTOs.DisplayVideo.Campaign;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// campaigns (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCampaignController : ControllerBase
{
    private readonly IDisplayVideoCampaignService _campaignService;

    public DisplayVideoCampaignController(IDisplayVideoCampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    /// <summary>Creates a new campaign.</summary>
    [HttpPost("advertisers/{advertiserId}/campaigns")]
    public async Task<IActionResult> CreateCampaignAsync([FromRoute] string advertiserId, [FromBody] CreateCampaignRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.CreateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists campaigns in an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/campaigns")]
    public async Task<IActionResult> ListCampaignsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCampaignsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken, Filter = filter };
        var result = await _campaignService.ListCampaignsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing campaign.</summary>
    [HttpPatch("advertisers/{advertiserId}/campaigns/{campaignId}")]
    public async Task<IActionResult> UpdateCampaignAsync([FromRoute] string advertiserId, [FromRoute] string campaignId, [FromBody] UpdateCampaignRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.CampaignId = campaignId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.UpdateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a campaign.</summary>
    [HttpGet("advertisers/{advertiserId}/campaigns/{campaignId}")]
    public async Task<IActionResult> GetCampaignAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string campaignId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCampaignRequest { AccountKey = accountKey, AdvertiserId = advertiserId, CampaignId = campaignId };
        var result = await _campaignService.GetCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Permanently deletes a campaign.</summary>
    [HttpDelete("advertisers/{advertiserId}/campaigns/{campaignId}")]
    public async Task<IActionResult> DeleteCampaignAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string campaignId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteCampaignRequest { AccountKey = accountKey, AdvertiserId = advertiserId, CampaignId = campaignId };
        var result = await _campaignService.DeleteCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

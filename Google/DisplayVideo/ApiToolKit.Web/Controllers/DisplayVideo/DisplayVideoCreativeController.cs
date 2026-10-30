using ApiToolKit.Application.DTOs.DisplayVideo.Creative;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// creatives (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCreativeController : ControllerBase
{
    private readonly IDisplayVideoCreativeService _creativeService;

    public DisplayVideoCreativeController(IDisplayVideoCreativeService creativeService)
    {
        _creativeService = creativeService;
    }

    /// <summary>Updates an existing creative.</summary>
    [HttpPatch("advertisers/{advertiserId}/creatives/{creativeId}")]
    public async Task<IActionResult> UpdateCreativeAsync([FromRoute] string advertiserId, [FromRoute] string creativeId, [FromBody] UpdateCreativeRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.CreativeId = creativeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeService.UpdateCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new creative.</summary>
    [HttpPost("advertisers/{advertiserId}/creatives")]
    public async Task<IActionResult> CreateCreativeAsync([FromRoute] string advertiserId, [FromBody] CreateCreativeRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeService.CreateCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a creative.</summary>
    [HttpGet("advertisers/{advertiserId}/creatives/{creativeId}")]
    public async Task<IActionResult> GetCreativeAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string creativeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCreativeRequest { AccountKey = accountKey, AdvertiserId = advertiserId, CreativeId = creativeId };
        var result = await _creativeService.GetCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists creatives in an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/creatives")]
    public async Task<IActionResult> ListCreativesAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCreativesRequest { AccountKey = accountKey, AdvertiserId = advertiserId, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken, Filter = filter };
        var result = await _creativeService.ListCreativesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a creative.</summary>
    [HttpDelete("advertisers/{advertiserId}/creatives/{creativeId}")]
    public async Task<IActionResult> DeleteCreativeAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string creativeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteCreativeRequest { AccountKey = accountKey, AdvertiserId = advertiserId, CreativeId = creativeId };
        var result = await _creativeService.DeleteCreativeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

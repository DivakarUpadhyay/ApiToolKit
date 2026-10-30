using ApiToolKit.Application.DTOs.DisplayVideo.Advertiser;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// advertisers (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdvertiserController : ControllerBase
{
    private readonly IDisplayVideoAdvertiserService _advertiserService;

    public DisplayVideoAdvertiserController(IDisplayVideoAdvertiserService advertiserService)
    {
        _advertiserService = advertiserService;
    }

    /// <summary>Gets an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}")]
    public async Task<IActionResult> GetAdvertiserAsync([FromQuery] string accountKey, [FromRoute] string advertiserId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdvertiserRequest { AccountKey = accountKey, AdvertiserId = advertiserId };
        var result = await _advertiserService.GetAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists assigned targeting options of an advertiser across targeting types.</summary>
    [HttpGet("advertisers/{advertiserId}/listAssignedTargetingOptions")]
    public async Task<IActionResult> ListAssignedTargetingOptionsAdvertisersAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedTargetingOptionsAdvertisersRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy };
        var result = await _advertiserService.ListAssignedTargetingOptionsAdvertisersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Edits targeting options under a single advertiser.</summary>
    [HttpPost("advertisers/{advertiserId}/editAssignedTargetingOptions")]
    public async Task<IActionResult> EditAssignedTargetingOptionsAdvertiserAsync([FromRoute] string advertiserId, [FromBody] EditAssignedTargetingOptionsAdvertiserRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.EditAssignedTargetingOptionsAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new advertiser.</summary>
    [HttpPost("advertisers")]
    public async Task<IActionResult> CreateAdvertiserAsync([FromBody] CreateAdvertiserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.CreateAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing advertiser.</summary>
    [HttpPatch("advertisers/{advertiserId}")]
    public async Task<IActionResult> UpdateAdvertiserAsync([FromRoute] string advertiserId, [FromBody] UpdateAdvertiserRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.UpdateAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Audits an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/audit")]
    public async Task<IActionResult> AuditAdvertiserAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? readMask = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new AuditAdvertiserRequest { AccountKey = accountKey, AdvertiserId = advertiserId, ReadMask = readMask };
        var result = await _advertiserService.AuditAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists advertisers that are accessible to the current user.</summary>
    [HttpGet("advertisers")]
    public async Task<IActionResult> ListAdvertisersAsync([FromQuery] string accountKey, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdvertisersRequest { AccountKey = accountKey, Filter = filter, PageSize = pageSize, OrderBy = orderBy, PartnerId = partnerId, PageToken = pageToken };
        var result = await _advertiserService.ListAdvertisersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an advertiser.</summary>
    [HttpDelete("advertisers/{advertiserId}")]
    public async Task<IActionResult> DeleteAdvertiserAsync([FromQuery] string accountKey, [FromRoute] string advertiserId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAdvertiserRequest { AccountKey = accountKey, AdvertiserId = advertiserId };
        var result = await _advertiserService.DeleteAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

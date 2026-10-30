using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupAd;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// adGroupAds (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdGroupAdController : ControllerBase
{
    private readonly IDisplayVideoAdGroupAdService _adGroupAdService;

    public DisplayVideoAdGroupAdController(IDisplayVideoAdGroupAdService adGroupAdService)
    {
        _adGroupAdService = adGroupAdService;
    }

    /// <summary>Deletes an ad group ad.</summary>
    [HttpDelete("advertisers/{advertiserId}/adGroupAds/{adGroupAdId}")]
    public async Task<IActionResult> DeleteAdGroupAdAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupAdId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAdGroupAdRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupAdId = adGroupAdId };
        var result = await _adGroupAdService.DeleteAdGroupAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an ad group ad.</summary>
    [HttpPatch("advertisers/{advertiserId}/adGroupAds/{adGroupAdId}")]
    public async Task<IActionResult> UpdateAdGroupAdAsync([FromRoute] string advertiserId, [FromRoute] string adGroupAdId, [FromBody] UpdateAdGroupAdRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.AdGroupAdId = adGroupAdId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupAdService.UpdateAdGroupAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an ad group ad.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroupAds/{adGroupAdId}")]
    public async Task<IActionResult> GetAdGroupAdAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupAdId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdGroupAdRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupAdId = adGroupAdId };
        var result = await _adGroupAdService.GetAdGroupAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists ad group ads.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroupAds")]
    public async Task<IActionResult> ListAdGroupAdsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdGroupAdsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, PageToken = pageToken, OrderBy = orderBy, PageSize = pageSize };
        var result = await _adGroupAdService.ListAdGroupAdsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates an ad group ad.</summary>
    [HttpPost("advertisers/{advertiserId}/adGroupAds")]
    public async Task<IActionResult> CreateAdGroupAdAsync([FromRoute] string advertiserId, [FromBody] CreateAdGroupAdRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupAdService.CreateAdGroupAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

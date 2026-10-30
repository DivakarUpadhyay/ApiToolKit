using ApiToolKit.Application.DTOs.DisplayVideo.AdAsset;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// adAssets (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdAssetController : ControllerBase
{
    private readonly IDisplayVideoAdAssetService _adAssetService;

    public DisplayVideoAdAssetController(IDisplayVideoAdAssetService adAssetService)
    {
        _adAssetService = adAssetService;
    }

    /// <summary>Lists ad assets under an advertiser ID.</summary>
    [HttpGet("advertisers/{advertiserId}/adAssets")]
    public async Task<IActionResult> ListAdAssetsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdAssetsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken };
        var result = await _adAssetService.ListAdAssetsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates multiple ad assets in a single request.</summary>
    [HttpPost("advertisers/{advertiserId}/adAssets/bulkCreate")]
    public async Task<IActionResult> BulkCreateAdAssetAsync([FromRoute] string advertiserId, [FromBody] BulkCreateAdAssetRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAssetService.BulkCreateAdAssetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates an ad asset.</summary>
    [HttpPost("advertisers/{advertiserId}/adAssets")]
    public async Task<IActionResult> CreateAdAssetAsync([FromRoute] string advertiserId, [FromBody] CreateAdAssetRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAssetService.CreateAdAssetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an ad asset.</summary>
    [HttpGet("advertisers/{advertiserId}/adAssets/{adAssetId}")]
    public async Task<IActionResult> GetAdAssetAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adAssetId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdAssetRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdAssetId = adAssetId };
        var result = await _adAssetService.GetAdAssetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an ad asset.</summary>
    [HttpPatch("advertisers/{advertiserId}/adAssets/{adAssetId}")]
    public async Task<IActionResult> UpdateAdAssetAsync([FromRoute] string advertiserId, [FromRoute] string adAssetId, [FromBody] UpdateAdAssetRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.AdAssetId = adAssetId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAssetService.UpdateAdAssetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

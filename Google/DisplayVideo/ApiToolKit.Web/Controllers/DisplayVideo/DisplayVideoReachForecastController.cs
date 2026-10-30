using ApiToolKit.Application.DTOs.DisplayVideo.ReachForecast;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// reachForecast (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoReachForecastController : ControllerBase
{
    private readonly IDisplayVideoReachForecastService _reachForecastService;

    public DisplayVideoReachForecastController(IDisplayVideoReachForecastService reachForecastService)
    {
        _reachForecastService = reachForecastService;
    }

    /// <summary>Retrieves the list of products that can be planned for a location.</summary>
    [HttpGet("advertisers/{advertiserId}/reachForecast/retrievePlannableProducts")]
    public async Task<IActionResult> RetrievePlannableProductsReachForecastAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? plannableLocationId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrievePlannableProductsReachForecastRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PlannableLocationId = plannableLocationId };
        var result = await _reachForecastService.RetrievePlannableProductsReachForecastAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates a reach forecast for a given advertiser and targeting configuration.</summary>
    [HttpPost("advertisers/{advertiserId}/reachForecast/generateReachForecast")]
    public async Task<IActionResult> GenerateReachForecastAsync([FromRoute] string advertiserId, [FromBody] GenerateReachForecastRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reachForecastService.GenerateReachForecastAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves first and third party user lists available for forecasting.</summary>
    [HttpGet("advertisers/{advertiserId}/reachForecast/retrievePlannableUserLists")]
    public async Task<IActionResult> RetrievePlannableUserListsReachForecastAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrievePlannableUserListsReachForecastRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, PageToken = pageToken, Filter = filter };
        var result = await _reachForecastService.RetrievePlannableUserListsReachForecastAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves Google Audiences (User Interests) available for forecasting.</summary>
    [HttpGet("advertisers/{advertiserId}/reachForecast/retrievePlannableUserInterests")]
    public async Task<IActionResult> RetrievePlannableUserInterestsReachForecastAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? productCategory = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrievePlannableUserInterestsReachForecastRequest { AccountKey = accountKey, AdvertiserId = advertiserId, ProductCategory = productCategory };
        var result = await _reachForecastService.RetrievePlannableUserInterestsReachForecastAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of countries where reach forecasting is supported.</summary>
    [HttpGet("advertisers/{advertiserId}/reachForecast/retrievePlannableLocations")]
    public async Task<IActionResult> RetrievePlannableLocationsReachForecastAsync([FromQuery] string accountKey, [FromRoute] string advertiserId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrievePlannableLocationsReachForecastRequest { AccountKey = accountKey, AdvertiserId = advertiserId };
        var result = await _reachForecastService.RetrievePlannableLocationsReachForecastAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

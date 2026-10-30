using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Planning methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestPlanningController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestPlanningController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns the list of plannable locations (for example, countries).</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListPlannableLocationsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listPlannableLocations")]
    public async Task<IActionResult> ListPlannableLocationsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListPlannableLocations", HttpMethod.Post, $":listPlannableLocations", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of plannable user lists with their plannable status.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListPlannableUserListsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listPlannableUserLists")]
    public async Task<IActionResult> ListPlannableUserListsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListPlannableUserLists", HttpMethod.Post, $":listPlannableUserLists", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of products that supports benchmarks.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListBenchmarksProductsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listBenchmarksProducts")]
    public async Task<IActionResult> ListBenchmarksProductsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListBenchmarksProducts", HttpMethod.Post, $":listBenchmarksProducts", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of per-location plannable YouTube ad formats with allowed targeting.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListPlannableProductsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listPlannableProducts")]
    public async Task<IActionResult> ListPlannableProductsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListPlannableProducts", HttpMethod.Post, $":listPlannableProducts", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of plannable user interests.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListPlannableUserInterestsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listPlannableUserInterests")]
    public async Task<IActionResult> ListPlannableUserInterestsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListPlannableUserInterests", HttpMethod.Post, $":listPlannableUserInterests", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of locations that support benchmarks (for example, countries).</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListBenchmarksLocationsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listBenchmarksLocations")]
    public async Task<IActionResult> ListBenchmarksLocationsAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListBenchmarksLocations", HttpMethod.Post, $":listBenchmarksLocations", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the list of benchmarks sources (for example, Industry Verticals).</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListBenchmarksSourcesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listBenchmarksSources")]
    public async Task<IActionResult> ListBenchmarksSourcesAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListBenchmarksSources", HttpMethod.Post, $":listBenchmarksSources", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a date range that supports benchmarks.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListBenchmarksAvailableDatesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("listBenchmarksAvailableDates")]
    public async Task<IActionResult> ListBenchmarksAvailableDatesAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.ListBenchmarksAvailableDates", HttpMethod.Post, $":listBenchmarksAvailableDates", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a collection of conversion rate suggestions for supported plannable products.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateConversionRatesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("generateConversionRates")]
    public async Task<IActionResult> GenerateConversionRatesAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Planning.GenerateConversionRates", HttpMethod.Post, $":generateConversionRates", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

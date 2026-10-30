using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Incentives methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestIncentivesController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestIncentivesController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Applies the incentive for the ads customer.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ApplyIncentiveRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/incentives/{incentiveId}/applyIncentive")]
    public async Task<IActionResult> ApplyIncentiveAsync([FromRoute] string customerId, [FromRoute] string incentiveId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Incentives.ApplyIncentive", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/incentives/{Uri.EscapeDataString(incentiveId)}:applyIncentive", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns incentives for a given user.</summary>
    [HttpGet("incentives/fetchIncentive")]
    public async Task<IActionResult> FetchIncentiveAsync([FromQuery] string customerId, [FromQuery(Name = "countryCode")] string? countryCode = null, [FromQuery(Name = "languageCode")] string? languageCode = null, [FromQuery(Name = "email")] string? email = null, [FromQuery(Name = "incentiveType")] string? incentiveType = null)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("countryCode", countryCode));
        query.Add(new KeyValuePair<string, string?>("languageCode", languageCode));
        query.Add(new KeyValuePair<string, string?>("email", email));
        query.Add(new KeyValuePair<string, string?>("incentiveType", incentiveType));
        var result = await _restService.SendAsync(customerId, "Incentives.FetchIncentive", HttpMethod.Get, $"incentives:fetchIncentive", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

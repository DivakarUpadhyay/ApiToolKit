using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - GoogleAdsFields methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestGoogleAdsFieldsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestGoogleAdsFieldsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns just the requested field.</summary>
    [HttpGet("googleAdsFields/{googleAdsFieldId}")]
    public async Task<IActionResult> GetAsync([FromRoute] string googleAdsFieldId, [FromQuery] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "GoogleAdsFields.Get", HttpMethod.Get, $"googleAdsFields/{Uri.EscapeDataString(googleAdsFieldId)}", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns all fields that match the search query.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SearchGoogleAdsFieldsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("googleAdsFields/search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "GoogleAdsFields.Search", HttpMethod.Post, $"googleAdsFields:search", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

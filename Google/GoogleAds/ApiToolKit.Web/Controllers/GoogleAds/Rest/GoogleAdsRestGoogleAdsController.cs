using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - GoogleAds methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestGoogleAdsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestGoogleAdsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns all rows that match the search query.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SearchGoogleAdsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/googleAds/search")]
    public async Task<IActionResult> SearchAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "GoogleAds.Search", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/googleAds:search", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns all rows that match the search stream query.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SearchGoogleAdsStreamRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/googleAds/searchStream")]
    public async Task<IActionResult> SearchStreamAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "GoogleAds.SearchStream", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/googleAds:searchStream", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Executes mutate and actions operations.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateGoogleAdsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/googleAds/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "GoogleAds.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/googleAds:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

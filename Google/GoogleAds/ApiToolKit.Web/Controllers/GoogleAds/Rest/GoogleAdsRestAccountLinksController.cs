using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AccountLinks methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAccountLinksController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAccountLinksController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates an account link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateAccountLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/accountLinks/create")]
    public async Task<IActionResult> CreateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AccountLinks.Create", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/accountLinks:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates or removes an account link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAccountLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/accountLinks/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AccountLinks.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/accountLinks:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

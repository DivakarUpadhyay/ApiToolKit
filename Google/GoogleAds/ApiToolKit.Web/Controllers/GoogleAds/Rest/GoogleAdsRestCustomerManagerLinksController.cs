using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - CustomerManagerLinks methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCustomerManagerLinksController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCustomerManagerLinksController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Updates customer manager links.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCustomerManagerLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/customerManagerLinks/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CustomerManagerLinks.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/customerManagerLinks:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Moves a client customer to a new manager customer.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MoveManagerLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/customerManagerLinks/moveManagerLink")]
    public async Task<IActionResult> MoveManagerLinkAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CustomerManagerLinks.MoveManagerLink", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/customerManagerLinks:moveManagerLink", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

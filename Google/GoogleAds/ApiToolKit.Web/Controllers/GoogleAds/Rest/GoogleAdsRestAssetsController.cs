using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Assets methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAssetsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAssetsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates assets.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAssetsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/assets/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Assets.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/assets:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

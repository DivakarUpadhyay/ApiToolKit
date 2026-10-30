using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AdGroupAds methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAdGroupAdsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAdGroupAdsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes ads.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAdGroupAdsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/adGroupAds/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AdGroupAds.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/adGroupAds:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove text customization (formerly automatically created assets) from an ad.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveAutomaticallyCreatedAssetsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/adGroupAds/{adGroupAdId}/removeAutomaticallyCreatedAssets")]
    public async Task<IActionResult> RemoveAutomaticallyCreatedAssetsAsync([FromRoute] string customerId, [FromRoute] string adGroupAdId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AdGroupAds.RemoveAutomaticallyCreatedAssets", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/adGroupAds/{Uri.EscapeDataString(adGroupAdId)}:removeAutomaticallyCreatedAssets", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

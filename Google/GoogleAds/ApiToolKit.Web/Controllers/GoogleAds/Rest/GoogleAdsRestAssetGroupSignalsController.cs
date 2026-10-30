using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AssetGroupSignals methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAssetGroupSignalsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAssetGroupSignalsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates or removes asset group signals.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAssetGroupSignalsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/assetGroupSignals/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AssetGroupSignals.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/assetGroupSignals:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

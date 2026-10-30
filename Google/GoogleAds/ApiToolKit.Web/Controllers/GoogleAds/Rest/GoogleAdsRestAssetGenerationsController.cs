using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AssetGenerations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAssetGenerationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAssetGenerationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Uses generative AI to generate text that can be used as assets in a campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateTextRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/assetGenerations/generateText")]
    public async Task<IActionResult> GenerateTextAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AssetGenerations.GenerateText", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/assetGenerations:generateText", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Uses generative AI to generate images that can be used as assets in a campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateImagesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/assetGenerations/generateImages")]
    public async Task<IActionResult> GenerateImagesAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AssetGenerations.GenerateImages", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/assetGenerations:generateImages", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Media methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestMediaController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestMediaController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Uploads a video to Google-managed or advertiser owned (brand) YouTube channel.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateYouTubeVideoUploadRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/youTubeVideoUploads/create")]
    public async Task<IActionResult> UploadAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Media.Upload", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/youTubeVideoUploads:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

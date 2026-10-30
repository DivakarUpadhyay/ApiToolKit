using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - YouTubeVideoUploads methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestYouTubeVideoUploadsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestYouTubeVideoUploadsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Updates YouTube video's metadata, but only for videos uploaded using this API.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UpdateYouTubeVideoUploadRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/youTubeVideoUploads/update")]
    public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "YouTubeVideoUploads.Update", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/youTubeVideoUploads:update", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Removes YouTube videos uploaded using this API.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveYouTubeVideoUploadRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/youTubeVideoUploads/remove")]
    public async Task<IActionResult> RemoveAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "YouTubeVideoUploads.Remove", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/youTubeVideoUploads:remove", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

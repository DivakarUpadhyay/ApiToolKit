using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - DataLinks methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestDataLinksController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestDataLinksController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Update a data link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UpdateDataLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/dataLinks/update")]
    public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "DataLinks.Update", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/dataLinks:update", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a data link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateDataLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/dataLinks/create")]
    public async Task<IActionResult> CreateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "DataLinks.Create", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/dataLinks:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a data link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveDataLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/dataLinks/remove")]
    public async Task<IActionResult> RemoveAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "DataLinks.Remove", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/dataLinks:remove", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

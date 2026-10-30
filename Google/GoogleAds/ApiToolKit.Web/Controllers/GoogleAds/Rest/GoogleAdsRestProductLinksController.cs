using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - ProductLinks methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestProductLinksController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestProductLinksController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates a product link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateProductLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/productLinks/create")]
    public async Task<IActionResult> CreateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ProductLinks.Create", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/productLinks:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Removes a product link.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveProductLinkRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/productLinks/remove")]
    public async Task<IActionResult> RemoveAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ProductLinks.Remove", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/productLinks:remove", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - ProductLinkInvitations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestProductLinkInvitationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestProductLinkInvitationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Update a product link invitation.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UpdateProductLinkInvitationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/productLinkInvitations/update")]
    public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ProductLinkInvitations.Update", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/productLinkInvitations:update", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a product link invitation.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateProductLinkInvitationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/productLinkInvitations/create")]
    public async Task<IActionResult> CreateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ProductLinkInvitations.Create", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/productLinkInvitations:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a product link invitation.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveProductLinkInvitationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/productLinkInvitations/remove")]
    public async Task<IActionResult> RemoveAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ProductLinkInvitations.Remove", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/productLinkInvitations:remove", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

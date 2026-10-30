using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - CustomerUserAccessInvitations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCustomerUserAccessInvitationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCustomerUserAccessInvitationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates or removes an access invitation.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCustomerUserAccessInvitationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/customerUserAccessInvitations/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CustomerUserAccessInvitations.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/customerUserAccessInvitations:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

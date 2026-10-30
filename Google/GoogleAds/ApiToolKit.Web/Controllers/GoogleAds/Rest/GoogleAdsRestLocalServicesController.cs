using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - LocalServices methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestLocalServicesController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestLocalServicesController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>RPC to append Local Services Lead Conversation resources to Local Services Lead resources.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__AppendLeadConversationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/localServices/appendLeadConversation")]
    public async Task<IActionResult> AppendLeadConversationAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "LocalServices.AppendLeadConversation", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/localServices:appendLeadConversation", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

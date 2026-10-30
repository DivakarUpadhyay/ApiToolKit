using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - LocalServicesLeads methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestLocalServicesLeadsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestLocalServicesLeadsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>RPC to provide feedback on Local Services Lead resources.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ProvideLeadFeedbackRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/localServicesLeads/{localServicesLeadId}/provideLeadFeedback")]
    public async Task<IActionResult> ProvideLeadFeedbackAsync([FromRoute] string customerId, [FromRoute] string localServicesLeadId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "LocalServicesLeads.ProvideLeadFeedback", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/localServicesLeads/{Uri.EscapeDataString(localServicesLeadId)}:provideLeadFeedback", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

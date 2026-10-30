using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - OfflineUserDataJobs methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestOfflineUserDataJobsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestOfflineUserDataJobsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Adds operations to the offline user data job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__AddOfflineUserDataJobOperationsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/offlineUserDataJobs/{offlineUserDataJobId}/addOperations")]
    public async Task<IActionResult> AddOperationsAsync([FromRoute] string customerId, [FromRoute] string offlineUserDataJobId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "OfflineUserDataJobs.AddOperations", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/offlineUserDataJobs/{Uri.EscapeDataString(offlineUserDataJobId)}:addOperations", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Runs the offline user data job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RunOfflineUserDataJobRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/offlineUserDataJobs/{offlineUserDataJobId}/run")]
    public async Task<IActionResult> RunAsync([FromRoute] string customerId, [FromRoute] string offlineUserDataJobId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "OfflineUserDataJobs.Run", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/offlineUserDataJobs/{Uri.EscapeDataString(offlineUserDataJobId)}:run", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates an offline user data job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateOfflineUserDataJobRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/offlineUserDataJobs/create")]
    public async Task<IActionResult> CreateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "OfflineUserDataJobs.Create", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/offlineUserDataJobs:create", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

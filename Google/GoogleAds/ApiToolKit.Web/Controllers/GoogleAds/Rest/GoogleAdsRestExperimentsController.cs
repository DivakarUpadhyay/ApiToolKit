using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Experiments methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestExperimentsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestExperimentsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Schedule an experiment.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ScheduleExperimentRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/experiments/{experimentId}/scheduleExperiment")]
    public async Task<IActionResult> ScheduleExperimentAsync([FromRoute] string customerId, [FromRoute] string experimentId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Experiments.ScheduleExperiment", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/experiments/{Uri.EscapeDataString(experimentId)}:scheduleExperiment", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns all errors that occurred during the last Experiment update (either scheduling or promotion).</summary>
    [HttpGet("customers/{customerId}/experiments/{experimentId}/listExperimentAsyncErrors")]
    public async Task<IActionResult> ListExperimentAsyncErrorsAsync([FromRoute] string customerId, [FromRoute] string experimentId, [FromQuery(Name = "pageSize")] string? pageSize = null, [FromQuery(Name = "pageToken")] string? pageToken = null)
    {
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("pageSize", pageSize));
        query.Add(new KeyValuePair<string, string?>("pageToken", pageToken));
        var result = await _restService.SendAsync(customerId, "Experiments.ListExperimentAsyncErrors", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/experiments/{Uri.EscapeDataString(experimentId)}:listExperimentAsyncErrors", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Immediately ends an experiment, changing the experiment's scheduled end date and without waiting for end of day.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__EndExperimentRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/experiments/{experimentId}/endExperiment")]
    public async Task<IActionResult> EndExperimentAsync([FromRoute] string customerId, [FromRoute] string experimentId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Experiments.EndExperiment", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/experiments/{Uri.EscapeDataString(experimentId)}:endExperiment", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Promotes the trial campaign thus applying changes in the trial campaign to the base campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__PromoteExperimentRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/experiments/{experimentId}/promoteExperiment")]
    public async Task<IActionResult> PromoteExperimentAsync([FromRoute] string customerId, [FromRoute] string experimentId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Experiments.PromoteExperiment", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/experiments/{Uri.EscapeDataString(experimentId)}:promoteExperiment", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Graduates an experiment to a full campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GraduateExperimentRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/experiments/{experimentId}/graduateExperiment")]
    public async Task<IActionResult> GraduateExperimentAsync([FromRoute] string customerId, [FromRoute] string experimentId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Experiments.GraduateExperiment", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/experiments/{Uri.EscapeDataString(experimentId)}:graduateExperiment", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates, updates, or removes experiments.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateExperimentsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/experiments/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Experiments.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/experiments:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - BatchJobs methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestBatchJobsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestBatchJobsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Add operations to the batch job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__AddBatchJobOperationsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/batchJobs/{batchJobId}/addOperations")]
    public async Task<IActionResult> AddOperationsAsync([FromRoute] string customerId, [FromRoute] string batchJobId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "BatchJobs.AddOperations", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/batchJobs/{Uri.EscapeDataString(batchJobId)}:addOperations", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Runs the batch job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RunBatchJobRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/batchJobs/{batchJobId}/run")]
    public async Task<IActionResult> RunAsync([FromRoute] string customerId, [FromRoute] string batchJobId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "BatchJobs.Run", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/batchJobs/{Uri.EscapeDataString(batchJobId)}:run", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Mutates a batch job.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateBatchJobRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/batchJobs/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "BatchJobs.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/batchJobs:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the results of the batch job.</summary>
    [HttpGet("customers/{customerId}/batchJobs/{batchJobId}/listResults")]
    public async Task<IActionResult> ListResultsAsync([FromRoute] string customerId, [FromRoute] string batchJobId, [FromQuery(Name = "pageSize")] string? pageSize = null, [FromQuery(Name = "responseContentType")] string? responseContentType = null, [FromQuery(Name = "pageToken")] string? pageToken = null)
    {
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("pageSize", pageSize));
        query.Add(new KeyValuePair<string, string?>("responseContentType", responseContentType));
        query.Add(new KeyValuePair<string, string?>("pageToken", pageToken));
        var result = await _restService.SendAsync(customerId, "BatchJobs.ListResults", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/batchJobs/{Uri.EscapeDataString(batchJobId)}:listResults", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Operations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestOperationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestOperationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Gets the latest state of a long-running operation.</summary>
    [HttpGet("customers/{customerId}/operations/{operationId}")]
    public async Task<IActionResult> GetAsync([FromRoute] string customerId, [FromRoute] string operationId)
    {
        var result = await _restService.SendAsync(customerId, "Operations.Get", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/operations/{Uri.EscapeDataString(operationId)}", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists operations that match the specified filter in the request.</summary>
    [HttpGet("customers/{customerId}/operations")]
    public async Task<IActionResult> ListAsync([FromRoute] string customerId, [FromQuery(Name = "pageSize")] string? pageSize = null, [FromQuery(Name = "returnPartialSuccess")] string? returnPartialSuccess = null, [FromQuery(Name = "pageToken")] string? pageToken = null, [FromQuery(Name = "filter")] string? filter = null)
    {
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("pageSize", pageSize));
        query.Add(new KeyValuePair<string, string?>("returnPartialSuccess", returnPartialSuccess));
        query.Add(new KeyValuePair<string, string?>("pageToken", pageToken));
        query.Add(new KeyValuePair<string, string?>("filter", filter));
        var result = await _restService.SendAsync(customerId, "Operations.List", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/operations", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a long-running operation.</summary>
    [HttpDelete("customers/{customerId}/operations/{operationId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string customerId, [FromRoute] string operationId)
    {
        var result = await _restService.SendAsync(customerId, "Operations.Delete", HttpMethod.Delete, $"customers/{Uri.EscapeDataString(customerId)}/operations/{Uri.EscapeDataString(operationId)}", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Waits until the specified long-running operation is done or reaches at most a specified timeout, returning the latest state.</summary>
    /// <remarks>Body: GoogleLongrunning__WaitOperationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/operations/{operationId}/wait")]
    public async Task<IActionResult> WaitAsync([FromRoute] string customerId, [FromRoute] string operationId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Operations.Wait", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/operations/{Uri.EscapeDataString(operationId)}:wait", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Starts asynchronous cancellation on a long-running operation.</summary>
    /// <remarks>Body: GoogleLongrunning__CancelOperationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/operations/{operationId}/cancel")]
    public async Task<IActionResult> CancelAsync([FromRoute] string customerId, [FromRoute] string operationId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Operations.Cancel", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/operations/{Uri.EscapeDataString(operationId)}:cancel", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}

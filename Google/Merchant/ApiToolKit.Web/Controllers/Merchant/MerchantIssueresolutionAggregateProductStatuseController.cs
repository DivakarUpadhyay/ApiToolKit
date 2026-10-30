using ApiToolKit.Application.DTOs.Merchant.IssueresolutionAggregateProductStatuse;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// aggregateProductStatuses (v1)
/// </summary>
[ApiController]
[Route("api/merchant/issueresolution")]
[Produces("application/json")]
public class MerchantIssueresolutionAggregateProductStatuseController : ControllerBase
{
    private readonly IMerchantIssueresolutionAggregateProductStatuseService _issueresolutionAggregateProductStatuseService;

    public MerchantIssueresolutionAggregateProductStatuseController(IMerchantIssueresolutionAggregateProductStatuseService issueresolutionAggregateProductStatuseService)
    {
        _issueresolutionAggregateProductStatuseService = issueresolutionAggregateProductStatuseService;
    }

    /// <summary>Lists the `AggregateProductStatuses` resources for your merchant account.</summary>
    [HttpGet("accounts/{accountId}/aggregateProductStatuses")]
    public async Task<IActionResult> ListAggregateProductStatusesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAggregateProductStatusesRequest { AccountKey = accountKey, AccountId = accountId, PageToken = pageToken, PageSize = pageSize, Filter = filter };
        var result = await _issueresolutionAggregateProductStatuseService.ListAggregateProductStatusesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

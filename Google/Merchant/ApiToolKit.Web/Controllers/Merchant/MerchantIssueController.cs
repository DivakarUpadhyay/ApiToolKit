using ApiToolKit.Application.DTOs.Merchant.Issue;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// issues (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantIssueController : ControllerBase
{
    private readonly IMerchantIssueService _issueService;

    public MerchantIssueController(IMerchantIssueService issueService)
    {
        _issueService = issueService;
    }

    /// <summary>Lists all account issues of a Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/issues")]
    public async Task<IActionResult> ListIssuesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? timeZone = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? languageCode = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListIssuesRequest { AccountKey = accountKey, AccountId = accountId, TimeZone = timeZone, PageToken = pageToken, PageSize = pageSize, LanguageCode = languageCode };
        var result = await _issueService.ListIssuesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

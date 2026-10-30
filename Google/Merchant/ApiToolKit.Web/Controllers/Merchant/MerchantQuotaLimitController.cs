using ApiToolKit.Application.DTOs.Merchant.QuotaLimit;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// limits (v1)
/// </summary>
[ApiController]
[Route("api/merchant/quota")]
[Produces("application/json")]
public class MerchantQuotaLimitController : ControllerBase
{
    private readonly IMerchantQuotaLimitService _quotaLimitService;

    public MerchantQuotaLimitController(IMerchantQuotaLimitService quotaLimitService)
    {
        _quotaLimitService = quotaLimitService;
    }

    /// <summary>Lists the limits of an account.</summary>
    [HttpGet("accounts/{accountId}/limits")]
    public async Task<IActionResult> ListLimitsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLimitsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken, Filter = filter };
        var result = await _quotaLimitService.ListLimitsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves an account limit.</summary>
    [HttpGet("accounts/{accountId}/limits/{limitId}")]
    public async Task<IActionResult> GetLimitAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string limitId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLimitRequest { AccountKey = accountKey, AccountId = accountId, LimitId = limitId };
        var result = await _quotaLimitService.GetLimitAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

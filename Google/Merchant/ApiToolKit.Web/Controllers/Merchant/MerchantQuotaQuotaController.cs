using ApiToolKit.Application.DTOs.Merchant.QuotaQuota;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// quotas (v1)
/// </summary>
[ApiController]
[Route("api/merchant/quota")]
[Produces("application/json")]
public class MerchantQuotaQuotaController : ControllerBase
{
    private readonly IMerchantQuotaQuotaService _quotaQuotaService;

    public MerchantQuotaQuotaController(IMerchantQuotaQuotaService quotaQuotaService)
    {
        _quotaQuotaService = quotaQuotaService;
    }

    /// <summary>Lists the daily call quota and usage per group for your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/quotas")]
    public async Task<IActionResult> ListQuotasAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListQuotasRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _quotaQuotaService.ListQuotasAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

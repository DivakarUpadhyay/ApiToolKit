using ApiToolKit.Application.DTOs.Merchant.TermsOfService;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// termsOfService (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantTermsOfServiceController : ControllerBase
{
    private readonly IMerchantTermsOfServiceService _termsOfServiceService;

    public MerchantTermsOfServiceController(IMerchantTermsOfServiceService termsOfServiceService)
    {
        _termsOfServiceService = termsOfServiceService;
    }

    /// <summary>Retrieves the latest version of the `TermsOfService` for a given `kind` and `region_code`.</summary>
    [HttpGet("termsOfService/retrieveLatest")]
    public async Task<IActionResult> RetrieveLatestTermsOfServiceAsync([FromQuery] string accountKey, [FromQuery] string? regionCode = null, [FromQuery] string? kind = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrieveLatestTermsOfServiceRequest { AccountKey = accountKey, RegionCode = regionCode, Kind = kind };
        var result = await _termsOfServiceService.RetrieveLatestTermsOfServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Accepts a `TermsOfService`.</summary>
    [HttpPost("termsOfService/{termsOfServiceId}/accept")]
    public async Task<IActionResult> AcceptTermsOfServiceAsync([FromRoute] string termsOfServiceId, [FromBody] AcceptTermsOfServiceRequest request)
    {
        request.TermsOfServiceId = termsOfServiceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _termsOfServiceService.AcceptTermsOfServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the `TermsOfService` associated with the provided version.</summary>
    [HttpGet("termsOfService/{termsOfServiceId}")]
    public async Task<IActionResult> GetTermsOfServiceAsync([FromQuery] string accountKey, [FromRoute] string termsOfServiceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTermsOfServiceRequest { AccountKey = accountKey, TermsOfServiceId = termsOfServiceId };
        var result = await _termsOfServiceService.GetTermsOfServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

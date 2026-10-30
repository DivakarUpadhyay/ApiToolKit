using ApiToolKit.Application.DTOs.Merchant.TermsOfServiceAgreementState;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// termsOfServiceAgreementStates (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantTermsOfServiceAgreementStateController : ControllerBase
{
    private readonly IMerchantTermsOfServiceAgreementStateService _termsOfServiceAgreementStateService;

    public MerchantTermsOfServiceAgreementStateController(IMerchantTermsOfServiceAgreementStateService termsOfServiceAgreementStateService)
    {
        _termsOfServiceAgreementStateService = termsOfServiceAgreementStateService;
    }

    /// <summary>Retrieves the state of the agreement for the application terms of service.</summary>
    [HttpGet("accounts/{accountId}/termsOfServiceAgreementStates/retrieveForApplication")]
    public async Task<IActionResult> RetrieveForApplicationTermsOfServiceAgreementStateAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RetrieveForApplicationTermsOfServiceAgreementStateRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _termsOfServiceAgreementStateService.RetrieveForApplicationTermsOfServiceAgreementStateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the state of a terms of service agreement.</summary>
    [HttpGet("accounts/{accountId}/termsOfServiceAgreementStates/{termsOfServiceAgreementStateId}")]
    public async Task<IActionResult> GetTermsOfServiceAgreementStateAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string termsOfServiceAgreementStateId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTermsOfServiceAgreementStateRequest { AccountKey = accountKey, AccountId = accountId, TermsOfServiceAgreementStateId = termsOfServiceAgreementStateId };
        var result = await _termsOfServiceAgreementStateService.GetTermsOfServiceAgreementStateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

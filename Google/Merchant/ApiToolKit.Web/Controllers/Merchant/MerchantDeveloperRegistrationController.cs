using ApiToolKit.Application.DTOs.Merchant.DeveloperRegistration;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// developerRegistration (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantDeveloperRegistrationController : ControllerBase
{
    private readonly IMerchantDeveloperRegistrationService _developerRegistrationService;

    public MerchantDeveloperRegistrationController(IMerchantDeveloperRegistrationService developerRegistrationService)
    {
        _developerRegistrationService = developerRegistrationService;
    }

    /// <summary>Retrieves a developer registration for a merchant.</summary>
    [HttpGet("accounts/{accountId}/developerRegistration")]
    public async Task<IActionResult> GetDeveloperRegistrationAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetDeveloperRegistrationRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _developerRegistrationService.GetDeveloperRegistrationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the merchant account that the calling GCP is registered with.</summary>
    [HttpGet("accounts/getAccountForGcpRegistration")]
    public async Task<IActionResult> GetAccountForGcpRegistrationDeveloperRegistrationAsync([FromQuery] string accountKey)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountForGcpRegistrationDeveloperRegistrationRequest { AccountKey = accountKey };
        var result = await _developerRegistrationService.GetAccountForGcpRegistrationDeveloperRegistrationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Unregister the calling GCP from the calling shopping account.</summary>
    [HttpPost("accounts/{accountId}/developerRegistration/unregisterGcp")]
    public async Task<IActionResult> UnregisterGcpDeveloperRegistrationAsync([FromRoute] string accountId, [FromBody] UnregisterGcpDeveloperRegistrationRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _developerRegistrationService.UnregisterGcpDeveloperRegistrationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Registers the GCP used for the API call to the shopping account passed in the request.</summary>
    [HttpPost("accounts/{accountId}/developerRegistration/registerGcp")]
    public async Task<IActionResult> RegisterGcpDeveloperRegistrationAsync([FromRoute] string accountId, [FromBody] RegisterGcpDeveloperRegistrationRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _developerRegistrationService.RegisterGcpDeveloperRegistrationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

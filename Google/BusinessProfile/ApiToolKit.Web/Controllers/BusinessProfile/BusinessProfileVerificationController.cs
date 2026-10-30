using ApiToolKit.Application.DTOs.BusinessProfile.Verification;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Location verification (Verifications API).
/// </summary>
[ApiController]
[Route("api/businessprofile/verifications")]
[Produces("application/json")]
public class BusinessProfileVerificationController : ControllerBase
{
    private readonly IBusinessProfileVerificationService _verificationService;

    public BusinessProfileVerificationController(IBusinessProfileVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    /// <summary>List verification methods available for a location.</summary>
    [HttpPost("options")]
    public async Task<IActionResult> FetchVerificationOptionsAsync([FromBody] FetchVerificationOptionsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _verificationService.FetchVerificationOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Start verification of a location.</summary>
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyLocationAsync([FromBody] VerifyLocationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _verificationService.VerifyLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List verifications of a location.</summary>
    [HttpGet]
    public async Task<IActionResult> ListVerificationsAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListVerificationsRequest { AccountKey = accountKey, LocationId = locationId, PageSize = pageSize, PageToken = pageToken };
        var result = await _verificationService.ListVerificationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Complete a pending verification with the PIN.</summary>
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteVerificationAsync([FromBody] CompleteVerificationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _verificationService.CompleteVerificationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Check whether the location is verified and can be edited.</summary>
    [HttpGet("voice-of-merchant")]
    public async Task<IActionResult> GetVoiceOfMerchantStateAsync([FromQuery] string accountKey, [FromQuery] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetVoiceOfMerchantStateRequest { AccountKey = accountKey, LocationId = locationId };
        var result = await _verificationService.GetVoiceOfMerchantStateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

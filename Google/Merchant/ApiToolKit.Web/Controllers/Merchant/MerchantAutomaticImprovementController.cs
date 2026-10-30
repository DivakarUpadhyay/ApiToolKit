using ApiToolKit.Application.DTOs.Merchant.AutomaticImprovement;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// automaticImprovements (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantAutomaticImprovementController : ControllerBase
{
    private readonly IMerchantAutomaticImprovementService _automaticImprovementService;

    public MerchantAutomaticImprovementController(IMerchantAutomaticImprovementService automaticImprovementService)
    {
        _automaticImprovementService = automaticImprovementService;
    }

    /// <summary>Retrieves the automatic improvements of an account.</summary>
    [HttpGet("accounts/{accountId}/automaticImprovements")]
    public async Task<IActionResult> GetAutomaticImprovementsAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAutomaticImprovementsRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _automaticImprovementService.GetAutomaticImprovementsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the automatic improvements of an account.</summary>
    [HttpPatch("accounts/{accountId}/automaticImprovements")]
    public async Task<IActionResult> UpdateAutomaticImprovementsAsync([FromRoute] string accountId, [FromBody] UpdateAutomaticImprovementsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _automaticImprovementService.UpdateAutomaticImprovementsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

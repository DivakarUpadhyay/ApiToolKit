using ApiToolKit.Application.DTOs.Merchant.OnlineReturnPolicy;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// onlineReturnPolicies (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantOnlineReturnPolicyController : ControllerBase
{
    private readonly IMerchantOnlineReturnPolicyService _onlineReturnPolicyService;

    public MerchantOnlineReturnPolicyController(IMerchantOnlineReturnPolicyService onlineReturnPolicyService)
    {
        _onlineReturnPolicyService = onlineReturnPolicyService;
    }

    /// <summary>Lists all existing return policies for a given business.</summary>
    [HttpGet("accounts/{accountId}/onlineReturnPolicies")]
    public async Task<IActionResult> ListOnlineReturnPoliciesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListOnlineReturnPoliciesRequest { AccountKey = accountKey, AccountId = accountId, PageToken = pageToken, PageSize = pageSize };
        var result = await _onlineReturnPolicyService.ListOnlineReturnPoliciesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new return policy for a given business.</summary>
    [HttpPost("accounts/{accountId}/onlineReturnPolicies")]
    public async Task<IActionResult> CreateOnlineReturnPolicyAsync([FromRoute] string accountId, [FromBody] CreateOnlineReturnPolicyRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _onlineReturnPolicyService.CreateOnlineReturnPolicyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an existing return policy for a given business.</summary>
    [HttpGet("accounts/{accountId}/onlineReturnPolicies/{onlineReturnPolicyId}")]
    public async Task<IActionResult> GetOnlineReturnPolicyAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string onlineReturnPolicyId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOnlineReturnPolicyRequest { AccountKey = accountKey, AccountId = accountId, OnlineReturnPolicyId = onlineReturnPolicyId };
        var result = await _onlineReturnPolicyService.GetOnlineReturnPolicyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing return policy.</summary>
    [HttpDelete("accounts/{accountId}/onlineReturnPolicies/{onlineReturnPolicyId}")]
    public async Task<IActionResult> DeleteOnlineReturnPolicyAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string onlineReturnPolicyId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteOnlineReturnPolicyRequest { AccountKey = accountKey, AccountId = accountId, OnlineReturnPolicyId = onlineReturnPolicyId };
        var result = await _onlineReturnPolicyService.DeleteOnlineReturnPolicyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

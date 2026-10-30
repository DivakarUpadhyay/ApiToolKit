using ApiToolKit.Application.DTOs.Merchant.Service;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// services (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantServiceController : ControllerBase
{
    private readonly IMerchantServiceService _serviceService;

    public MerchantServiceController(IMerchantServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    /// <summary>Reject an account service (both proposed and approve services can be rejected).</summary>
    [HttpPost("accounts/{accountId}/services/{serviceId}/reject")]
    public async Task<IActionResult> RejectServiceAsync([FromRoute] string accountId, [FromRoute] string serviceId, [FromBody] RejectServiceRequest request)
    {
        request.AccountId = accountId;
        request.ServiceId = serviceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceService.RejectServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Approve an account service proposal.</summary>
    [HttpPost("accounts/{accountId}/services/{serviceId}/approve")]
    public async Task<IActionResult> ApproveServiceAsync([FromRoute] string accountId, [FromRoute] string serviceId, [FromBody] ApproveServiceRequest request)
    {
        request.AccountId = accountId;
        request.ServiceId = serviceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceService.ApproveServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Propose an account service.</summary>
    [HttpPost("accounts/{accountId}/services/propose")]
    public async Task<IActionResult> ProposeServiceAsync([FromRoute] string accountId, [FromBody] ProposeServiceRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceService.ProposeServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieve an account service.</summary>
    [HttpGet("accounts/{accountId}/services/{serviceId}")]
    public async Task<IActionResult> GetServiceAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string serviceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetServiceRequest { AccountKey = accountKey, AccountId = accountId, ServiceId = serviceId };
        var result = await _serviceService.GetServiceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List account services for the specified accounts.</summary>
    [HttpGet("accounts/{accountId}/services")]
    public async Task<IActionResult> ListServicesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListServicesRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _serviceService.ListServicesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

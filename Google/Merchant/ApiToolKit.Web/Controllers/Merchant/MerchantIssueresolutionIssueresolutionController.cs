using ApiToolKit.Application.DTOs.Merchant.IssueresolutionIssueresolution;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// issueresolution (v1)
/// </summary>
[ApiController]
[Route("api/merchant/issueresolution")]
[Produces("application/json")]
public class MerchantIssueresolutionIssueresolutionController : ControllerBase
{
    private readonly IMerchantIssueresolutionIssueresolutionService _issueresolutionIssueresolutionService;

    public MerchantIssueresolutionIssueresolutionController(IMerchantIssueresolutionIssueresolutionService issueresolutionIssueresolutionService)
    {
        _issueresolutionIssueresolutionService = issueresolutionIssueresolutionService;
    }

    /// <summary>Start an action.</summary>
    [HttpPost("accounts/{accountId}/triggeraction")]
    public async Task<IActionResult> TriggeractionIssueresolutionAsync([FromRoute] string accountId, [FromBody] TriggeractionIssueresolutionRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _issueresolutionIssueresolutionService.TriggeractionIssueresolutionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Provide a list of business's account issues with an issue resolution content and available actions.</summary>
    [HttpPost("accounts/{accountId}/renderaccountissues")]
    public async Task<IActionResult> RenderaccountissuesIssueresolutionAsync([FromRoute] string accountId, [FromBody] RenderaccountissuesIssueresolutionRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _issueresolutionIssueresolutionService.RenderaccountissuesIssueresolutionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Provide a list of issues for business's product with an issue resolution content and available actions.</summary>
    [HttpPost("accounts/{accountId}/products/{productId}/renderproductissues")]
    public async Task<IActionResult> RenderproductissuesIssueresolutionAsync([FromRoute] string accountId, [FromRoute] string productId, [FromBody] RenderproductissuesIssueresolutionRequest request)
    {
        request.AccountId = accountId;
        request.ProductId = productId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _issueresolutionIssueresolutionService.RenderproductissuesIssueresolutionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Facebook.AdAccount;
using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/ad-account")]
[Produces("application/json")]
public class FacebookAdAccountController : ControllerBase
{
    private readonly IFacebookAdAccountService _adAccountService;

    public FacebookAdAccountController(IFacebookAdAccountService adAccountService)
    {
        _adAccountService = adAccountService;
    }

    /// <summary>Get ad account details by account ID</summary>
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAdAccountAsync(string accountId, [FromQuery] string accessToken)
    {
        var result = await _adAccountService.GetAdAccountAsync(accountId, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad accounts associated with a user</summary>
    [HttpPost("user/ad-accounts")]
    public async Task<IActionResult> GetAdAccountsByUserAsync([FromBody] GetAdAccountsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAccountService.GetAdAccountsByUserAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get billing information for an ad account</summary>
    [HttpGet("{accountId}/billing")]
    public async Task<IActionResult> GetAdAccountBillingAsync(string accountId, [FromQuery] string accessToken)
    {
        var result = await _adAccountService.GetAdAccountBillingAsync(accountId, accessToken);
        return Ok(result);
    }

    /// <summary>Get users assigned to an ad account</summary>
    [HttpPost("{accountId}/users")]
    public async Task<IActionResult> GetAdAccountUsersAsync([FromBody] AdAccountUsersRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAccountService.GetAdAccountUsersAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get campaigns under an ad account</summary>
    [HttpPost("{accountId}/campaigns")]
    public async Task<IActionResult> GetAdAccountCampaignsAsync([FromBody] GetCampaignsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adAccountService.GetAdAccountCampaignsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad creatives under an ad account</summary>
    [HttpGet("{accountId}/creatives")]
    public async Task<IActionResult> GetAdAccountCreativesAsync(string accountId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _adAccountService.GetAdAccountCreativesAsync(accountId, accessToken, limit);
        return Ok(result);
    }
}

using ApiToolKit.Application.DTOs.Merchant.ConversionsConversionSource;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// conversionSources (v1)
/// </summary>
[ApiController]
[Route("api/merchant/conversions")]
[Produces("application/json")]
public class MerchantConversionsConversionSourceController : ControllerBase
{
    private readonly IMerchantConversionsConversionSourceService _conversionsConversionSourceService;

    public MerchantConversionsConversionSourceController(IMerchantConversionsConversionSourceService conversionsConversionSourceService)
    {
        _conversionsConversionSourceService = conversionsConversionSourceService;
    }

    /// <summary>Creates a new conversion source.</summary>
    [HttpPost("accounts/{accountId}/conversionSources")]
    public async Task<IActionResult> CreateConversionSourceAsync([FromRoute] string accountId, [FromBody] CreateConversionSourceRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionsConversionSourceService.CreateConversionSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates information of an existing conversion source.</summary>
    [HttpPatch("accounts/{accountId}/conversionSources/{conversionSourceId}")]
    public async Task<IActionResult> UpdateConversionSourceAsync([FromRoute] string accountId, [FromRoute] string conversionSourceId, [FromBody] UpdateConversionSourceRequest request)
    {
        request.AccountId = accountId;
        request.ConversionSourceId = conversionSourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionsConversionSourceService.UpdateConversionSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Archives an existing conversion source.</summary>
    [HttpDelete("accounts/{accountId}/conversionSources/{conversionSourceId}")]
    public async Task<IActionResult> DeleteConversionSourceAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string conversionSourceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteConversionSourceRequest { AccountKey = accountKey, AccountId = accountId, ConversionSourceId = conversionSourceId };
        var result = await _conversionsConversionSourceService.DeleteConversionSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Re-enables an archived conversion source.</summary>
    [HttpPost("accounts/{accountId}/conversionSources/{conversionSourceId}/undelete")]
    public async Task<IActionResult> UndeleteConversionSourceAsync([FromRoute] string accountId, [FromRoute] string conversionSourceId, [FromBody] UndeleteConversionSourceRequest request)
    {
        request.AccountId = accountId;
        request.ConversionSourceId = conversionSourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionsConversionSourceService.UndeleteConversionSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Fetches a conversion source.</summary>
    [HttpGet("accounts/{accountId}/conversionSources/{conversionSourceId}")]
    public async Task<IActionResult> GetConversionSourceAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string conversionSourceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetConversionSourceRequest { AccountKey = accountKey, AccountId = accountId, ConversionSourceId = conversionSourceId };
        var result = await _conversionsConversionSourceService.GetConversionSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of conversion sources the caller has access to.</summary>
    [HttpGet("accounts/{accountId}/conversionSources")]
    public async Task<IActionResult> ListConversionSourcesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] bool? showDeleted = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListConversionSourcesRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, ShowDeleted = showDeleted, PageToken = pageToken };
        var result = await _conversionsConversionSourceService.ListConversionSourcesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

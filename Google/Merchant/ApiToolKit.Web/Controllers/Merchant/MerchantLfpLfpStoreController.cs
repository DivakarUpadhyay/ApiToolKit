using ApiToolKit.Application.DTOs.Merchant.LfpLfpStore;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// lfpStores (v1)
/// </summary>
[ApiController]
[Route("api/merchant/lfp")]
[Produces("application/json")]
public class MerchantLfpLfpStoreController : ControllerBase
{
    private readonly IMerchantLfpLfpStoreService _lfpLfpStoreService;

    public MerchantLfpLfpStoreController(IMerchantLfpLfpStoreService lfpLfpStoreService)
    {
        _lfpLfpStoreService = lfpLfpStoreService;
    }

    /// <summary>Retrieves information about a store.</summary>
    [HttpGet("accounts/{accountId}/lfpStores/{lfpStoreId}")]
    public async Task<IActionResult> GetLfpStoreAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string lfpStoreId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLfpStoreRequest { AccountKey = accountKey, AccountId = accountId, LfpStoreId = lfpStoreId };
        var result = await _lfpLfpStoreService.GetLfpStoreAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a store for a target merchant.</summary>
    [HttpDelete("accounts/{accountId}/lfpStores/{lfpStoreId}")]
    public async Task<IActionResult> DeleteLfpStoreAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string lfpStoreId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteLfpStoreRequest { AccountKey = accountKey, AccountId = accountId, LfpStoreId = lfpStoreId };
        var result = await _lfpLfpStoreService.DeleteLfpStoreAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a store for the target merchant.</summary>
    [HttpPost("accounts/{accountId}/lfpStores/insert")]
    public async Task<IActionResult> InsertLfpStoreAsync([FromRoute] string accountId, [FromBody] InsertLfpStoreRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lfpLfpStoreService.InsertLfpStoreAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the stores of the target merchant, specified by the filter in `ListLfpStoresRequest`.</summary>
    [HttpGet("accounts/{accountId}/lfpStores")]
    public async Task<IActionResult> ListLfpStoresAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? targetAccount = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLfpStoresRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken, TargetAccount = targetAccount };
        var result = await _lfpLfpStoreService.ListLfpStoresAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

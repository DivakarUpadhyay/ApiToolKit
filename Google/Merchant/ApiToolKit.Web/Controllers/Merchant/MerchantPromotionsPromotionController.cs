using ApiToolKit.Application.DTOs.Merchant.PromotionsPromotion;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// promotions (v1)
/// </summary>
[ApiController]
[Route("api/merchant/promotions")]
[Produces("application/json")]
public class MerchantPromotionsPromotionController : ControllerBase
{
    private readonly IMerchantPromotionsPromotionService _promotionsPromotionService;

    public MerchantPromotionsPromotionController(IMerchantPromotionsPromotionService promotionsPromotionService)
    {
        _promotionsPromotionService = promotionsPromotionService;
    }

    /// <summary>Lists the promotions in your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/promotions")]
    public async Task<IActionResult> ListPromotionsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPromotionsRequest { AccountKey = accountKey, AccountId = accountId, PageToken = pageToken, PageSize = pageSize };
        var result = await _promotionsPromotionService.ListPromotionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the promotion from your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/promotions/{promotionId}")]
    public async Task<IActionResult> GetPromotionAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string promotionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPromotionRequest { AccountKey = accountKey, AccountId = accountId, PromotionId = promotionId };
        var result = await _promotionsPromotionService.GetPromotionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a promotion for your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/promotions/insert")]
    public async Task<IActionResult> InsertPromotionAsync([FromRoute] string accountId, [FromBody] InsertPromotionRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _promotionsPromotionService.InsertPromotionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

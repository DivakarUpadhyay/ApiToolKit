using ApiToolKit.Application.DTOs.Merchant.ReviewsMerchantReviewAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// merchantReviews (Alpha)
/// </summary>
[ApiController]
[Route("api/merchant/reviews-v1alpha")]
[Produces("application/json")]
public class MerchantReviewsMerchantReviewAlphaController : ControllerBase
{
    private readonly IMerchantReviewsMerchantReviewAlphaService _reviewsMerchantReviewAlphaService;

    public MerchantReviewsMerchantReviewAlphaController(IMerchantReviewsMerchantReviewAlphaService reviewsMerchantReviewAlphaService)
    {
        _reviewsMerchantReviewAlphaService = reviewsMerchantReviewAlphaService;
    }

    /// <summary>Deletes merchant review.</summary>
    [HttpDelete("accounts/{accountId}/merchantReviews/{merchantReviewId}")]
    public async Task<IActionResult> DeleteMerchantReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string merchantReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteMerchantReviewRequest { AccountKey = accountKey, AccountId = accountId, MerchantReviewId = merchantReviewId };
        var result = await _reviewsMerchantReviewAlphaService.DeleteMerchantReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a merchant review.</summary>
    [HttpGet("accounts/{accountId}/merchantReviews/{merchantReviewId}")]
    public async Task<IActionResult> GetMerchantReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string merchantReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetMerchantReviewRequest { AccountKey = accountKey, AccountId = accountId, MerchantReviewId = merchantReviewId };
        var result = await _reviewsMerchantReviewAlphaService.GetMerchantReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists merchant reviews.</summary>
    [HttpGet("accounts/{accountId}/merchantReviews")]
    public async Task<IActionResult> ListMerchantReviewsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListMerchantReviewsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _reviewsMerchantReviewAlphaService.ListMerchantReviewsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a review for your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/merchantReviews/insert")]
    public async Task<IActionResult> InsertMerchantReviewAsync([FromRoute] string accountId, [FromBody] InsertMerchantReviewRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reviewsMerchantReviewAlphaService.InsertMerchantReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

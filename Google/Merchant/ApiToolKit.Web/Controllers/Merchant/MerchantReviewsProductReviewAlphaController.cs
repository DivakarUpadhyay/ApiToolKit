using ApiToolKit.Application.DTOs.Merchant.ReviewsProductReviewAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// productReviews (Alpha)
/// </summary>
[ApiController]
[Route("api/merchant/reviews-v1alpha")]
[Produces("application/json")]
public class MerchantReviewsProductReviewAlphaController : ControllerBase
{
    private readonly IMerchantReviewsProductReviewAlphaService _reviewsProductReviewAlphaService;

    public MerchantReviewsProductReviewAlphaController(IMerchantReviewsProductReviewAlphaService reviewsProductReviewAlphaService)
    {
        _reviewsProductReviewAlphaService = reviewsProductReviewAlphaService;
    }

    /// <summary>Deletes a product review.</summary>
    [HttpDelete("accounts/{accountId}/productReviews/{productReviewId}")]
    public async Task<IActionResult> DeleteProductReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteProductReviewRequest { AccountKey = accountKey, AccountId = accountId, ProductReviewId = productReviewId };
        var result = await _reviewsProductReviewAlphaService.DeleteProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists product reviews.</summary>
    [HttpGet("accounts/{accountId}/productReviews")]
    public async Task<IActionResult> ListProductReviewsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListProductReviewsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _reviewsProductReviewAlphaService.ListProductReviewsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a product review.</summary>
    [HttpGet("accounts/{accountId}/productReviews/{productReviewId}")]
    public async Task<IActionResult> GetProductReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetProductReviewRequest { AccountKey = accountKey, AccountId = accountId, ProductReviewId = productReviewId };
        var result = await _reviewsProductReviewAlphaService.GetProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a product review.</summary>
    [HttpPost("accounts/{accountId}/productReviews/insert")]
    public async Task<IActionResult> InsertProductReviewAsync([FromRoute] string accountId, [FromBody] InsertProductReviewRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reviewsProductReviewAlphaService.InsertProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

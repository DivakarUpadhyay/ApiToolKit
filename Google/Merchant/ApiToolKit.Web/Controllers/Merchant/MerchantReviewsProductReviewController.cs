using ApiToolKit.Application.DTOs.Merchant.ReviewsProductReview;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// productReviews (v1)
/// </summary>
[ApiController]
[Route("api/merchant/reviews")]
[Produces("application/json")]
public class MerchantReviewsProductReviewController : ControllerBase
{
    private readonly IMerchantReviewsProductReviewService _reviewsProductReviewService;

    public MerchantReviewsProductReviewController(IMerchantReviewsProductReviewService reviewsProductReviewService)
    {
        _reviewsProductReviewService = reviewsProductReviewService;
    }

    /// <summary>Gets a product review.</summary>
    [HttpGet("accounts/{accountId}/productReviews/{productReviewId}")]
    public async Task<IActionResult> GetProductReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetProductReviewRequest { AccountKey = accountKey, AccountId = accountId, ProductReviewId = productReviewId };
        var result = await _reviewsProductReviewService.GetProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists product reviews.</summary>
    [HttpGet("accounts/{accountId}/productReviews")]
    public async Task<IActionResult> ListProductReviewsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListProductReviewsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _reviewsProductReviewService.ListProductReviewsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a product review.</summary>
    [HttpPost("accounts/{accountId}/productReviews/insert")]
    public async Task<IActionResult> InsertProductReviewAsync([FromRoute] string accountId, [FromBody] InsertProductReviewRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reviewsProductReviewService.InsertProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a product review.</summary>
    [HttpDelete("accounts/{accountId}/productReviews/{productReviewId}")]
    public async Task<IActionResult> DeleteProductReviewAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productReviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteProductReviewRequest { AccountKey = accountKey, AccountId = accountId, ProductReviewId = productReviewId };
        var result = await _reviewsProductReviewService.DeleteProductReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.BusinessProfile.Review;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Customer reviews and owner replies.
/// </summary>
[ApiController]
[Route("api/businessprofile/reviews")]
[Produces("application/json")]
public class BusinessProfileReviewController : ControllerBase
{
    private readonly IBusinessProfileReviewService _reviewService;

    public BusinessProfileReviewController(IBusinessProfileReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>List reviews for a location with average rating and total count.</summary>
    [HttpGet]
    public async Task<IActionResult> ListReviewsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListReviewsRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken };
        var result = await _reviewService.ListReviewsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one review.</summary>
    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromRoute] string reviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetReviewRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, ReviewId = reviewId };
        var result = await _reviewService.GetReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create or update the owner reply to a review.</summary>
    [HttpPost("reply")]
    public async Task<IActionResult> ReplyToReviewAsync([FromBody] ReplyToReviewRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reviewService.ReplyToReviewAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete the owner reply to a review.</summary>
    [HttpDelete("reply")]
    public async Task<IActionResult> DeleteReviewReplyAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] string reviewId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        if (string.IsNullOrEmpty(reviewId)) return BadRequest("reviewId is required.");
        var request = new DeleteReviewReplyRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, ReviewId = reviewId };
        var result = await _reviewService.DeleteReviewReplyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get reviews for several locations of an account in one call.</summary>
    [HttpPost("batch")]
    public async Task<IActionResult> BatchGetReviewsAsync([FromBody] BatchGetReviewsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reviewService.BatchGetReviewsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.BusinessProfile.LocalPost;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Local posts (updates, events, offers).
/// </summary>
[ApiController]
[Route("api/businessprofile/posts")]
[Produces("application/json")]
public class BusinessProfileLocalPostController : ControllerBase
{
    private readonly IBusinessProfileLocalPostService _localPostService;

    public BusinessProfileLocalPostController(IBusinessProfileLocalPostService localPostService)
    {
        _localPostService = localPostService;
    }

    /// <summary>List local posts for a location.</summary>
    [HttpGet]
    public async Task<IActionResult> ListLocalPostsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListLocalPostsRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, PageSize = pageSize, PageToken = pageToken };
        var result = await _localPostService.ListLocalPostsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Publish a local post on a location.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateLocalPostAsync([FromBody] CreateLocalPostRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _localPostService.CreateLocalPostAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a local post.</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteLocalPostAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] string postId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        if (string.IsNullOrEmpty(postId)) return BadRequest("postId is required.");
        var request = new DeleteLocalPostRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, PostId = postId };
        var result = await _localPostService.DeleteLocalPostAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one local post.</summary>
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetLocalPostAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromRoute] string postId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetLocalPostRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, PostId = postId };
        var result = await _localPostService.GetLocalPostAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a local post (fields in UpdateMask).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLocalPostAsync([FromBody] UpdateLocalPostRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _localPostService.UpdateLocalPostAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get insights (views, actions) for local posts.</summary>
    [HttpPost("insights")]
    public async Task<IActionResult> GetLocalPostInsightsAsync([FromBody] GetLocalPostInsightsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _localPostService.GetLocalPostInsightsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

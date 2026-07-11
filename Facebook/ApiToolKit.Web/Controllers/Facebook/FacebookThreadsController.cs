using ApiToolKit.Application.DTOs.Facebook.Threads;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/threads")]
[Produces("application/json")]
public class FacebookThreadsController : ControllerBase
{
    private readonly IFacebookThreadsService _service;

    public FacebookThreadsController(IFacebookThreadsService service) => _service = service;

    /// <summary>
    /// Retrieves the authenticated user's Threads profile information.
    /// </summary>
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile([FromQuery] string accessToken)
    {
        var result = await _service.GetProfileAsync(accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the authenticated user's Threads posts.
    /// </summary>
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts([FromQuery] string accessToken, [FromQuery] int limit = 20)
    {
        var result = await _service.GetPostsAsync(accessToken, limit);
        return Ok(result);
    }

    /// <summary>
    /// Creates and publishes a new text-only Threads post.
    /// </summary>
    [HttpPost("posts/text")]
    public async Task<IActionResult> CreateTextPost([FromBody] CreateTextThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateTextPostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates and publishes a new image Threads post.
    /// </summary>
    [HttpPost("posts/image")]
    public async Task<IActionResult> CreateImagePost([FromBody] CreateMediaThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateImagePostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates and publishes a new video Threads post.
    /// </summary>
    [HttpPost("posts/video")]
    public async Task<IActionResult> CreateVideoPost([FromBody] CreateMediaThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateVideoPostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates and publishes a new carousel Threads post containing multiple images.
    /// </summary>
    [HttpPost("posts/carousel")]
    public async Task<IActionResult> CreateCarouselPost([FromBody] CreateCarouselThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateCarouselPostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Publishes a previously created Threads media container by its creation ID.
    /// </summary>
    [HttpPost("posts/publish")]
    public async Task<IActionResult> PublishContainer([FromBody] PublishThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.PublishContainerAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a single Threads post by its ID.
    /// </summary>
    [HttpGet("posts/{threadId}")]
    public async Task<IActionResult> GetPost(string threadId, [FromQuery] string accessToken)
    {
        var result = await _service.GetPostAsync(threadId, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a Threads post by its ID.
    /// </summary>
    [HttpDelete("posts/{threadId}")]
    public async Task<IActionResult> DeletePost(string threadId, [FromQuery] string accessToken)
    {
        var result = await _service.DeletePostAsync(threadId, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates and publishes a text reply to a specific Threads post.
    /// </summary>
    [HttpPost("posts/{threadId}/reply")]
    public async Task<IActionResult> ReplyToPost(string threadId, [FromBody] CreateTextThreadRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.ReplyToPostAsync(threadId, request, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the direct replies to a specific Threads post.
    /// </summary>
    [HttpGet("posts/{threadId}/replies")]
    public async Task<IActionResult> GetReplies(string threadId, [FromQuery] string accessToken)
    {
        var result = await _service.GetRepliesAsync(threadId, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the full conversation thread for a specific Threads post.
    /// </summary>
    [HttpGet("posts/{threadId}/conversation")]
    public async Task<IActionResult> GetConversation(string threadId, [FromQuery] string accessToken)
    {
        var result = await _service.GetConversationAsync(threadId, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves account-level insights metrics for the authenticated Threads user.
    /// </summary>
    [HttpGet("insights")]
    public async Task<IActionResult> GetInsights([FromQuery] string accessToken)
    {
        var result = await _service.GetInsightsAsync(accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves engagement insights for a specific Threads post.
    /// </summary>
    [HttpGet("posts/{threadId}/insights")]
    public async Task<IActionResult> GetPostInsights(string threadId, [FromQuery] string accessToken)
    {
        var result = await _service.GetPostInsightsAsync(threadId, accessToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the publishing rate limit and quota usage for the authenticated Threads user.
    /// </summary>
    [HttpGet("publishing-limit")]
    public async Task<IActionResult> GetPublishingLimit([FromQuery] string accessToken)
    {
        var result = await _service.GetPublishingLimitAsync(accessToken);
        return Ok(result);
    }
}

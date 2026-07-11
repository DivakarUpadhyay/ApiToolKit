using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/post")]
[Produces("application/json")]
public class FacebookPostController : ControllerBase
{
    private readonly IFacebookPostService _postService;

    public FacebookPostController(IFacebookPostService postService)
    {
        _postService = postService;
    }

    /// <summary>Create a new post</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _postService.CreatePostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a post by its ID</summary>
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostAsync(string postId, [FromQuery] string accessToken)
    {
        var result = await _postService.GetPostAsync(postId, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing post</summary>
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _postService.UpdatePostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a post</summary>
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePostAsync(string postId, [FromQuery] string accessToken)
    {
        var result = await _postService.DeletePostAsync(postId, accessToken);
        return Ok(result);
    }

    /// <summary>Get the feed for a page or user object</summary>
    [HttpPost("feed")]
    public async Task<IActionResult> GetFeedAsync([FromBody] GetFeedRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _postService.GetFeedAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get comments on a post</summary>
    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> GetPostCommentsAsync([FromBody] GetCommentsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _postService.GetPostCommentsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get reactions on a post</summary>
    [HttpPost("{postId}/reactions")]
    public async Task<IActionResult> GetPostReactionsAsync([FromBody] GetReactionsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _postService.GetPostReactionsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Publish a scheduled post immediately</summary>
    [HttpPost("{postId}/publish")]
    public async Task<IActionResult> PublishScheduledPostAsync(string postId, [FromQuery] string accessToken)
    {
        var result = await _postService.PublishScheduledPostAsync(postId, accessToken);
        return Ok(result);
    }
}

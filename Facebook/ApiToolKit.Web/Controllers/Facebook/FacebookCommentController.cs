using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/comment")]
[Produces("application/json")]
public class FacebookCommentController : ControllerBase
{
    private readonly IFacebookCommentService _commentService;

    public FacebookCommentController(IFacebookCommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>Create a comment on an object</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.CreateCommentAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a comment by its ID</summary>
    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetCommentAsync(string commentId, [FromQuery] string accessToken)
    {
        var result = await _commentService.GetCommentAsync(commentId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all comments on an object</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetCommentsAsync([FromBody] GetCommentsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.GetCommentsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update a comment's message</summary>
    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateCommentAsync([FromBody] UpdateCommentRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.UpdateCommentAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a comment</summary>
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteCommentAsync(string commentId, [FromQuery] string accessToken)
    {
        var result = await _commentService.DeleteCommentAsync(commentId, accessToken);
        return Ok(result);
    }

    /// <summary>Hide or unhide a comment</summary>
    [HttpPost("{commentId}/hide")]
    public async Task<IActionResult> HideCommentAsync([FromBody] HideCommentRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.HideCommentAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get replies to a comment</summary>
    [HttpGet("{commentId}/replies")]
    public async Task<IActionResult> GetCommentRepliesAsync(string commentId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _commentService.GetCommentRepliesAsync(commentId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Like a comment</summary>
    [HttpPost("{commentId}/like")]
    public async Task<IActionResult> LikeCommentAsync(string commentId, [FromQuery] string accessToken)
    {
        var result = await _commentService.LikeCommentAsync(commentId, accessToken);
        return Ok(result);
    }
}

using ApiToolKit.Application.DTOs.YouTube.Comment;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/comments")]
[Produces("application/json")]
public class YouTubeCommentController : ControllerBase
{
    private readonly IYouTubeCommentService _commentService;

    public YouTubeCommentController(IYouTubeCommentService commentService)
    {
        _commentService = commentService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List comment threads for a video or channel</summary>
    [HttpGet("list")]
    public async Task<IActionResult> ListCommentThreads([FromQuery] ListCommentThreadsRequest request)
    {
        var result = await _commentService.ListCommentThreadsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert a top-level comment on a video</summary>
    [HttpPost("insert")]
    public async Task<IActionResult> InsertComment([FromBody] InsertCommentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.InsertCommentAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reply to an existing comment</summary>
    [HttpPost("reply")]
    public async Task<IActionResult> ReplyComment([FromBody] ReplyCommentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.ReplyCommentAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the text of a comment</summary>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.UpdateCommentAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a comment</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] string id)
    {
        var request = new DeleteCommentRequest { CommentId = id };
        var result = await _commentService.DeleteCommentAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set the moderation status of a comment</summary>
    [HttpPost("moderate")]
    public async Task<IActionResult> ModerateComment([FromBody] ModerationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentService.ModerateCommentAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

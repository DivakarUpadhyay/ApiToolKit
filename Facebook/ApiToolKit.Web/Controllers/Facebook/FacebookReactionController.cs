using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/reaction")]
[Produces("application/json")]
public class FacebookReactionController : ControllerBase
{
    private readonly IFacebookReactionService _reactionService;

    public FacebookReactionController(IFacebookReactionService reactionService)
    {
        _reactionService = reactionService;
    }

    /// <summary>Add a reaction to an object (post, comment, etc.)</summary>
    [HttpPost]
    public async Task<IActionResult> CreateReactionAsync([FromBody] CreateReactionRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reactionService.CreateReactionAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get reactions on an object</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetReactionsAsync([FromBody] GetReactionsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reactionService.GetReactionsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Remove a reaction from an object</summary>
    [HttpDelete("{objectId}")]
    public async Task<IActionResult> DeleteReactionAsync(string objectId, [FromQuery] string accessToken)
    {
        var result = await _reactionService.DeleteReactionAsync(objectId, accessToken);
        return Ok(result);
    }
}

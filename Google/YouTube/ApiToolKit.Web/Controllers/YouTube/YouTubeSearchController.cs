using ApiToolKit.Application.DTOs.YouTube.Search;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/search")]
[Produces("application/json")]
public class YouTubeSearchController : ControllerBase
{
    private readonly IYouTubeSearchService _searchService;

    public YouTubeSearchController(IYouTubeSearchService searchService)
    {
        _searchService = searchService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>Search YouTube for videos, channels, or playlists</summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _searchService.SearchAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

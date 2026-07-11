using ApiToolKit.Application.DTOs.YouTube.Playlist;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/playlists")]
[Produces("application/json")]
public class YouTubePlaylistController : ControllerBase
{
    private readonly IYouTubePlaylistService _playlistService;

    public YouTubePlaylistController(IYouTubePlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List playlists</summary>
    [HttpGet]
    public async Task<IActionResult> ListPlaylists([FromQuery] ListPlaylistsRequest request)
    {
        var result = await _playlistService.ListPlaylistsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a playlist by ID</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylist([FromRoute] string id)
    {
        var result = await _playlistService.GetPlaylistAsync(id, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new playlist</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylistRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _playlistService.CreatePlaylistAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing playlist</summary>
    [HttpPut]
    public async Task<IActionResult> UpdatePlaylist([FromBody] UpdatePlaylistRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _playlistService.UpdatePlaylistAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a playlist</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist([FromRoute] string id)
    {
        var request = new DeletePlaylistRequest { PlaylistId = id };
        var result = await _playlistService.DeletePlaylistAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

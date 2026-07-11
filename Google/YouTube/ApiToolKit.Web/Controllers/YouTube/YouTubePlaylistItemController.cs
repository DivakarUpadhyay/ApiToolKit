using ApiToolKit.Application.DTOs.YouTube.PlaylistItem;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/playlist-items")]
[Produces("application/json")]
public class YouTubePlaylistItemController : ControllerBase
{
    private readonly IYouTubePlaylistItemService _playlistItemService;

    public YouTubePlaylistItemController(IYouTubePlaylistItemService playlistItemService)
    {
        _playlistItemService = playlistItemService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List items in a playlist</summary>
    [HttpGet]
    public async Task<IActionResult> ListPlaylistItems([FromQuery] ListPlaylistItemsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _playlistItemService.ListPlaylistItemsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a video to a playlist</summary>
    [HttpPost]
    public async Task<IActionResult> AddPlaylistItem([FromBody] AddPlaylistItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _playlistItemService.AddPlaylistItemAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a playlist item position</summary>
    [HttpPut]
    public async Task<IActionResult> UpdatePlaylistItem([FromBody] UpdatePlaylistItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _playlistItemService.UpdatePlaylistItemAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove an item from a playlist</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylistItem([FromRoute] string id)
    {
        var request = new DeletePlaylistItemRequest { ItemId = id };
        var result = await _playlistItemService.DeletePlaylistItemAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

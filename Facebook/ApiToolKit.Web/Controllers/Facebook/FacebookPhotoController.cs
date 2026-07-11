using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/photo")]
[Produces("application/json")]
public class FacebookPhotoController : ControllerBase
{
    private readonly IFacebookPhotoService _photoService;

    public FacebookPhotoController(IFacebookPhotoService photoService)
    {
        _photoService = photoService;
    }

    /// <summary>Upload a photo to a page or album</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadPhotoAsync([FromBody] UploadPhotoRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _photoService.UploadPhotoAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get photo details by ID</summary>
    [HttpGet("{photoId}")]
    public async Task<IActionResult> GetPhotoAsync(string photoId, [FromQuery] string accessToken)
    {
        var result = await _photoService.GetPhotoAsync(photoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all photos for an object (page, user, album)</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetPhotosAsync([FromBody] GetPhotosRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _photoService.GetPhotosAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a photo</summary>
    [HttpDelete("{photoId}")]
    public async Task<IActionResult> DeletePhotoAsync(string photoId, [FromQuery] string accessToken)
    {
        var result = await _photoService.DeletePhotoAsync(photoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get albums for an object (page or user)</summary>
    [HttpGet("{pageId}/albums")]
    public async Task<IActionResult> GetAlbumsAsync(string pageId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _photoService.GetAlbumsAsync(pageId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Create a new album</summary>
    [HttpPost("album")]
    public async Task<IActionResult> CreateAlbumAsync([FromBody] CreateAlbumRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _photoService.CreateAlbumAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get photos inside an album</summary>
    [HttpGet("{albumId}/photos")]
    public async Task<IActionResult> GetAlbumPhotosAsync(string albumId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _photoService.GetAlbumPhotosAsync(albumId, accessToken, limit);
        return Ok(result);
    }
}

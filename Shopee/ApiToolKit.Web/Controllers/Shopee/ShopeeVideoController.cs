using ApiToolKit.Application.DTOs.Shopee.Video;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/video")]
[Produces("application/json")]
public class ShopeeVideoController : ControllerBase
{
    private readonly IShopeeVideoService _videoService;

    public ShopeeVideoController(IShopeeVideoService videoService)
    {
        _videoService = videoService;
    }

    /// <summary>Upload a video to the media space</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo([FromBody] UploadVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.UploadVideoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of videos in the media space</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetVideoList([FromQuery] GetVideoListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.GetVideoListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific video</summary>
    [HttpGet]
    public async Task<IActionResult> GetVideo([FromQuery] GetVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.GetVideoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete one or more videos from the media space</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteVideo([FromBody] DeleteVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.DeleteVideoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the title of a video</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.UpdateVideoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the upload status of a video</summary>
    [HttpGet("upload-status")]
    public async Task<IActionResult> GetVideoUploadStatus([FromQuery] GetVideoUploadStatusRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.GetVideoUploadStatusAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a video to a product listing</summary>
    [HttpPost("item")]
    public async Task<IActionResult> AddVideoToItem([FromBody] AddVideoToItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.AddVideoToItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a video from a product listing</summary>
    [HttpDelete("item")]
    public async Task<IActionResult> DeleteVideoFromItem([FromBody] DeleteVideoFromItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.DeleteVideoFromItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of videos attached to a product listing</summary>
    [HttpGet("item-list")]
    public async Task<IActionResult> GetItemVideoList([FromQuery] GetItemVideoListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.GetItemVideoListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

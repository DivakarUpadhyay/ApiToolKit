using ApiToolKit.Application.DTOs.Shopee.Media;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/media")]
[Produces("application/json")]
public class ShopeeMediaController : ControllerBase
{
    private readonly IShopeeMediaService _mediaService;

    public ShopeeMediaController(IShopeeMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    /// <summary>Upload an image file to Shopee media space</summary>
    [HttpPost("image/upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage([FromQuery] long shopId, IFormFile image)
    {
        if (image == null || image.Length == 0) return BadRequest("Image file is required.");
        using var ms = new MemoryStream();
        await image.CopyToAsync(ms);
        var request = new UploadImageRequest { ShopId = shopId, ImageBytes = ms.ToArray(), FileName = image.FileName };
        var result = await _mediaService.UploadImageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Upload an image from a URL to Shopee media space</summary>
    [HttpPost("image/upload-url")]
    public async Task<IActionResult> UploadImageByUrl([FromBody] UploadImageByUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _mediaService.UploadImageByUrlAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Upload a video part to Shopee media space</summary>
    [HttpPost("video/upload")]
    public async Task<IActionResult> UploadVideo([FromBody] UploadVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _mediaService.UploadVideoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

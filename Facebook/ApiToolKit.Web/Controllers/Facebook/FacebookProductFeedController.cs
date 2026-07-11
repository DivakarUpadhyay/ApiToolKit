using ApiToolKit.Application.DTOs.Facebook.ProductFeed;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

public class ProductFeedUploadRequest
{
    public IFormFile File { get; set; } = null!;
}

[ApiController]
[Route("api/facebook/product-feed")]
[Produces("application/json")]
public class FacebookProductFeedController : ControllerBase
{
    private readonly IFacebookProductFeedService _service;
    public FacebookProductFeedController(IFacebookProductFeedService service) => _service = service;

    /// <summary>Create a new product feed for a catalog.</summary>
    [HttpPost("{catalogId}/feeds")]
    public async Task<IActionResult> CreateProductFeed(string catalogId, [FromBody] CreateProductFeedRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateProductFeedAsync(catalogId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get all product feeds for a catalog.</summary>
    [HttpGet("{catalogId}/feeds")]
    public async Task<IActionResult> GetProductFeeds(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.GetProductFeedsAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific product feed by feed ID.</summary>
    [HttpGet("feeds/{feedId}")]
    public async Task<IActionResult> GetProductFeed(string feedId, [FromQuery] string accessToken)
    {
        var result = await _service.GetProductFeedAsync(feedId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a product feed by feed ID.</summary>
    [HttpDelete("feeds/{feedId}")]
    public async Task<IActionResult> DeleteProductFeed(string feedId, [FromQuery] string accessToken)
    {
        var result = await _service.DeleteProductFeedAsync(feedId, accessToken);
        return Ok(result);
    }

    /// <summary>Upload a feed file to a product feed (multipart/form-data).</summary>
    [HttpPost("feeds/{feedId}/upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFeedFile(string feedId, [FromQuery] string accessToken, [FromForm] ProductFeedUploadRequest model)
    {
        using var stream = model.File.OpenReadStream();
        var result = await _service.UploadFeedFileAsync(feedId, accessToken, stream, model.File.FileName);
        return Ok(result);
    }

    /// <summary>Get all uploads for a product feed.</summary>
    [HttpGet("feeds/{feedId}/uploads")]
    public async Task<IActionResult> GetFeedUploads(string feedId, [FromQuery] string accessToken)
    {
        var result = await _service.GetFeedUploadsAsync(feedId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific feed upload by upload ID.</summary>
    [HttpGet("uploads/{uploadId}")]
    public async Task<IActionResult> GetFeedUpload(string uploadId, [FromQuery] string accessToken)
    {
        var result = await _service.GetFeedUploadAsync(uploadId, accessToken);
        return Ok(result);
    }

    /// <summary>Get product feed item errors for a feed.</summary>
    [HttpGet("feeds/{feedId}/errors")]
    public async Task<IActionResult> GetFeedErrors(string feedId, [FromQuery] string accessToken)
    {
        var result = await _service.GetFeedErrorsAsync(feedId, accessToken);
        return Ok(result);
    }
}

using ApiToolKit.Application.DTOs.Cloud.Vision;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Cloud;

/// <summary>
/// Google Cloud Vision API endpoints for image analysis and annotation.
/// </summary>
[ApiController]
[Route("api/cloud/vision")]
public class VisionController : ControllerBase
{
    private readonly IVisionService _visionService;

    public VisionController(IVisionService visionService)
    {
        _visionService = visionService;
    }

    /// <summary>
    /// Annotates a single image with one or more features (labels, faces, text, etc.).
    /// </summary>
    [HttpPost("annotate")]
    public async Task<IActionResult> AnnotateImage([FromBody] AnnotateImageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _visionService.AnnotateImageAsync(request);
        if (!result.Success) return BadRequest(new { result.Message, result.ErrorCode });
        return Ok(result.Data);
    }

    /// <summary>
    /// Annotates multiple images in a single batch request.
    /// </summary>
    [HttpPost("batch-annotate")]
    public async Task<IActionResult> BatchAnnotateImages([FromBody] BatchAnnotateImagesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _visionService.BatchAnnotateImagesAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Asynchronously annotates a file (PDF, TIFF) stored in Google Cloud Storage.
    /// Returns a long-running operation name that can be polled for completion.
    /// </summary>
    [HttpPost("async-annotate")]
    public async Task<IActionResult> AsyncAnnotateFile([FromBody] AsyncAnnotateFileRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _visionService.AsyncAnnotateFileAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Gets the status of a long-running Vision API operation by operation name.
    /// </summary>
    [HttpGet("operations/{operationName}")]
    public async Task<IActionResult> GetOperation([FromRoute] string operationName, [FromQuery] string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
            return BadRequest(new { Message = "projectId query parameter is required." });

        var request = new GetOperationRequest
        {
            ProjectId = projectId,
            OperationName = operationName
        };

        var result = await _visionService.GetOperationAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }
}

using ApiToolKit.Application.DTOs.Workspace.Slides;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Slides;

/// <summary>Google Slides API</summary>
[ApiController]
[Route("api/workspace/slides")]
public class SlidesController : ControllerBase
{
    private readonly ISlidesService _service;

    public SlidesController(ISlidesService service)
    {
        _service = service;
    }

    /// <summary>Create a new presentation</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreatePresentation(string userId, [FromBody] CreatePresentationRequest request)
    {
        var result = await _service.CreatePresentationAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a presentation</summary>
    [HttpGet("{userId}/{presentationId}")]
    public async Task<IActionResult> GetPresentation(string userId, string presentationId)
    {
        var result = await _service.GetPresentationAsync(userId, new GetPresentationRequest { PresentationId = presentationId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a slide to a presentation</summary>
    [HttpPost("{userId}/addSlide")]
    public async Task<IActionResult> AddSlide(string userId, [FromBody] AddSlideRequest request)
    {
        var result = await _service.AddSlideAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a slide from a presentation</summary>
    [HttpPost("{userId}/deleteSlide")]
    public async Task<IActionResult> DeleteSlide(string userId, [FromBody] DeleteSlideRequest request)
    {
        var result = await _service.DeleteSlideAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert text into a shape on a slide</summary>
    [HttpPost("{userId}/insertText")]
    public async Task<IActionResult> InsertText(string userId, [FromBody] SlidesInsertTextRequest request)
    {
        var result = await _service.InsertTextAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replace all matching text in the presentation</summary>
    [HttpPost("{userId}/replaceText")]
    public async Task<IActionResult> ReplaceText(string userId, [FromBody] SlidesReplaceTextRequest request)
    {
        var result = await _service.ReplaceTextAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert an image onto a slide</summary>
    [HttpPost("{userId}/insertImage")]
    public async Task<IActionResult> InsertImage(string userId, [FromBody] InsertImageRequest request)
    {
        var result = await _service.InsertImageAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Duplicate a slide</summary>
    [HttpPost("{userId}/duplicateSlide")]
    public async Task<IActionResult> DuplicateSlide(string userId, [FromBody] DuplicateSlideRequest request)
    {
        var result = await _service.DuplicateSlideAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reorder slides in a presentation</summary>
    [HttpPost("{userId}/reorderSlides")]
    public async Task<IActionResult> ReorderSlides(string userId, [FromBody] ReorderSlidesRequest request)
    {
        var result = await _service.ReorderSlidesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

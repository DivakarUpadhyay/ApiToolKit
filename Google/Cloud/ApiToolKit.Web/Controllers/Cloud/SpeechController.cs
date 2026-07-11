using ApiToolKit.Application.DTOs.Cloud.Speech;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Cloud;

/// <summary>
/// Google Cloud Speech-to-Text API endpoints for audio transcription.
/// </summary>
[ApiController]
[Route("api/cloud/speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechService _speechService;

    public SpeechController(ISpeechService speechService)
    {
        _speechService = speechService;
    }

    /// <summary>
    /// Performs synchronous speech recognition on audio up to 1 minute in duration.
    /// </summary>
    [HttpPost("recognize")]
    public async Task<IActionResult> Recognize([FromBody] RecognizeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _speechService.RecognizeAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Starts an asynchronous long-running speech recognition job for audio longer than 1 minute.
    /// Returns an operation name to poll for completion.
    /// </summary>
    [HttpPost("long-running")]
    public async Task<IActionResult> LongRunningRecognize([FromBody] LongRunningRecognizeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _speechService.LongRunningRecognizeAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Gets the status of a long-running Speech API operation by operation name.
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

        var result = await _speechService.GetOperationAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }
}

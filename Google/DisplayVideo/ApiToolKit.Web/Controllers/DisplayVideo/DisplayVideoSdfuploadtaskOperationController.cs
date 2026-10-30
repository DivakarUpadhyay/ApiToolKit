using ApiToolKit.Application.DTOs.DisplayVideo.SdfuploadtaskOperation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// sdfuploadtasks / operations (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoSdfuploadtaskOperationController : ControllerBase
{
    private readonly IDisplayVideoSdfuploadtaskOperationService _sdfuploadtaskOperationService;

    public DisplayVideoSdfuploadtaskOperationController(IDisplayVideoSdfuploadtaskOperationService sdfuploadtaskOperationService)
    {
        _sdfuploadtaskOperationService = sdfuploadtaskOperationService;
    }

    /// <summary>Gets the latest state of an asynchronous SDF download task operation.</summary>
    [HttpGet("sdfuploadtasks/operations/{operationId}")]
    public async Task<IActionResult> GetOperationAsync([FromQuery] string accountKey, [FromRoute] string operationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOperationRequest { AccountKey = accountKey, OperationId = operationId };
        var result = await _sdfuploadtaskOperationService.GetOperationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

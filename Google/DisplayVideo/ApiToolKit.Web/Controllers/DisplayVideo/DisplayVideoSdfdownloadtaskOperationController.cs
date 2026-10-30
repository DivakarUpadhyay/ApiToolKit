using ApiToolKit.Application.DTOs.DisplayVideo.SdfdownloadtaskOperation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// sdfdownloadtasks / operations (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoSdfdownloadtaskOperationController : ControllerBase
{
    private readonly IDisplayVideoSdfdownloadtaskOperationService _sdfdownloadtaskOperationService;

    public DisplayVideoSdfdownloadtaskOperationController(IDisplayVideoSdfdownloadtaskOperationService sdfdownloadtaskOperationService)
    {
        _sdfdownloadtaskOperationService = sdfdownloadtaskOperationService;
    }

    /// <summary>Gets the latest state of an asynchronous SDF download task operation.</summary>
    [HttpGet("sdfdownloadtasks/operations/{operationId}")]
    public async Task<IActionResult> GetOperationAsync([FromQuery] string accountKey, [FromRoute] string operationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOperationRequest { AccountKey = accountKey, OperationId = operationId };
        var result = await _sdfdownloadtaskOperationService.GetOperationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

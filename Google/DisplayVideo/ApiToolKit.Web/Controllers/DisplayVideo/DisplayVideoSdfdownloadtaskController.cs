using ApiToolKit.Application.DTOs.DisplayVideo.Sdfdownloadtask;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// sdfdownloadtasks (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoSdfdownloadtaskController : ControllerBase
{
    private readonly IDisplayVideoSdfdownloadtaskService _sdfdownloadtaskService;

    public DisplayVideoSdfdownloadtaskController(IDisplayVideoSdfdownloadtaskService sdfdownloadtaskService)
    {
        _sdfdownloadtaskService = sdfdownloadtaskService;
    }

    /// <summary>Creates an SDF Download Task.</summary>
    [HttpPost("sdfdownloadtasks")]
    public async Task<IActionResult> CreateSdfdownloadtaskAsync([FromBody] CreateSdfdownloadtaskRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _sdfdownloadtaskService.CreateSdfdownloadtaskAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

using ApiToolKit.Application.DTOs.Facebook.WhatsAppFlows;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/flows")]
[Produces("application/json")]
public class FacebookWhatsAppFlowsController : ControllerBase
{
    private readonly IFacebookWhatsAppFlowsService _flowsService;

    public FacebookWhatsAppFlowsController(IFacebookWhatsAppFlowsService flowsService)
    {
        _flowsService = flowsService;
    }

    /// <summary>Get all WhatsApp Flows for a WABA</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetFlowsAsync([FromBody] GetWhatsAppFlowsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _flowsService.GetFlowsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific WhatsApp Flow</summary>
    [HttpGet("{flowId}")]
    public async Task<IActionResult> GetFlowAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.GetFlowAsync(flowId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a new WhatsApp Flow</summary>
    [HttpPost]
    public async Task<IActionResult> CreateFlowAsync([FromBody] CreateWhatsAppFlowRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _flowsService.CreateFlowAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update a WhatsApp Flow's metadata</summary>
    [HttpPut("{flowId}")]
    public async Task<IActionResult> UpdateFlowAsync([FromBody] UpdateWhatsAppFlowRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _flowsService.UpdateFlowAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a WhatsApp Flow</summary>
    [HttpDelete("{flowId}")]
    public async Task<IActionResult> DeleteFlowAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.DeleteFlowAsync(flowId, accessToken);
        return Ok(result);
    }

    /// <summary>Publish a WhatsApp Flow (makes it available to send)</summary>
    [HttpPost("{flowId}/publish")]
    public async Task<IActionResult> PublishFlowAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.PublishFlowAsync(flowId, accessToken);
        return Ok(result);
    }

    /// <summary>Deprecate a WhatsApp Flow</summary>
    [HttpPost("{flowId}/deprecate")]
    public async Task<IActionResult> DeprecateFlowAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.DeprecateFlowAsync(flowId, accessToken);
        return Ok(result);
    }

    /// <summary>Get assets for a WhatsApp Flow</summary>
    [HttpGet("{flowId}/assets")]
    public async Task<IActionResult> GetFlowAssetsAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.GetFlowAssetsAsync(flowId, accessToken);
        return Ok(result);
    }

    /// <summary>Upload Flow JSON for a WhatsApp Flow</summary>
    [HttpPost("{flowId}/assets")]
    public async Task<IActionResult> UploadFlowJsonAsync([FromBody] UploadFlowJsonRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _flowsService.UploadFlowJsonAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get preview URL for a WhatsApp Flow</summary>
    [HttpGet("{flowId}/preview")]
    public async Task<IActionResult> GetFlowPreviewAsync(string flowId, [FromQuery] string accessToken)
    {
        var result = await _flowsService.GetFlowPreviewAsync(flowId, accessToken);
        return Ok(result);
    }
}

using ApiToolKit.Application.DTOs.Facebook.LeadGen;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/leadgen")]
[Produces("application/json")]
public class FacebookLeadGenController : ControllerBase
{
    private readonly IFacebookLeadGenService _leadGenService;

    public FacebookLeadGenController(IFacebookLeadGenService leadGenService)
    {
        _leadGenService = leadGenService;
    }

    /// <summary>Get all lead generation forms for a page</summary>
    [HttpPost("forms/list")]
    public async Task<IActionResult> GetLeadGenFormsAsync([FromBody] GetLeadGenFormsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _leadGenService.GetLeadGenFormsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific lead generation form</summary>
    [HttpGet("forms/{formId}")]
    public async Task<IActionResult> GetLeadGenFormAsync(string formId, [FromQuery] string accessToken)
    {
        var result = await _leadGenService.GetLeadGenFormAsync(formId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a lead generation form for a page</summary>
    [HttpPost("forms")]
    public async Task<IActionResult> CreateLeadGenFormAsync([FromBody] CreateLeadGenFormRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _leadGenService.CreateLeadGenFormAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Archive (delete) a lead generation form</summary>
    [HttpDelete("forms/{formId}")]
    public async Task<IActionResult> DeleteLeadGenFormAsync(string formId, [FromQuery] string accessToken)
    {
        var result = await _leadGenService.DeleteLeadGenFormAsync(formId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all leads collected by a form</summary>
    [HttpPost("leads/list")]
    public async Task<IActionResult> GetLeadsAsync([FromBody] GetLeadsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _leadGenService.GetLeadsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific lead by ID</summary>
    [HttpGet("leads/{leadId}")]
    public async Task<IActionResult> GetLeadAsync(string leadId, [FromQuery] string accessToken)
    {
        var result = await _leadGenService.GetLeadAsync(leadId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a specific lead</summary>
    [HttpDelete("leads/{leadId}")]
    public async Task<IActionResult> DeleteLeadAsync(string leadId, [FromQuery] string accessToken)
    {
        var result = await _leadGenService.DeleteLeadAsync(leadId, accessToken);
        return Ok(result);
    }
}

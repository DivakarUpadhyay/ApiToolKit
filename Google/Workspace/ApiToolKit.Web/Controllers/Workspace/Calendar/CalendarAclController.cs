using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Calendar;

/// <summary>Google Calendar ACL API</summary>
[ApiController]
[Route("api/workspace/calendar/acl")]
public class CalendarAclController : ControllerBase
{
    private readonly ICalendarAclService _service;

    public CalendarAclController(ICalendarAclService service)
    {
        _service = service;
    }

    /// <summary>List ACL rules for a calendar</summary>
    [HttpGet("{userId}/{calendarId}")]
    public async Task<IActionResult> ListAclRules(string userId, string calendarId)
    {
        var result = await _service.ListAclRulesAsync(userId, calendarId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific ACL rule</summary>
    [HttpGet("{userId}/{calendarId}/{ruleId}")]
    public async Task<IActionResult> GetAclRule(string userId, string calendarId, string ruleId)
    {
        var result = await _service.GetAclRuleAsync(userId, calendarId, ruleId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create an ACL rule</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateAclRule(string userId, [FromBody] CreateAclRuleRequest request)
    {
        var result = await _service.CreateAclRuleAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an ACL rule</summary>
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAclRule(string userId, [FromBody] UpdateAclRuleRequest request)
    {
        var result = await _service.UpdateAclRuleAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an ACL rule</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAclRule(string userId, [FromBody] DeleteAclRuleRequest request)
    {
        var result = await _service.DeleteAclRuleAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

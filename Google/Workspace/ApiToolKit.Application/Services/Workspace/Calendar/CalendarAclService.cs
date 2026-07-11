using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Calendar;

public class CalendarAclService : ICalendarAclService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/calendar/v3/calendars";

    public CalendarAclService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListAclResponse>> ListAclRulesAsync(string userId, string calendarId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListAclResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListAclResponse>($"{BaseUrl}/{Uri.EscapeDataString(calendarId)}/acl", accessToken);
        if (response == null) return Result<ListAclResponse>.Fail("No response from Calendar API.");

        return Result<ListAclResponse>.Ok(response);
    }

    public async Task<Result<AclRuleResponse>> GetAclRuleAsync(string userId, string calendarId, string ruleId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<AclRuleResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<AclRuleResponse>($"{BaseUrl}/{Uri.EscapeDataString(calendarId)}/acl/{ruleId}", accessToken);
        if (response == null) return Result<AclRuleResponse>.Fail("ACL rule not found.");

        return Result<AclRuleResponse>.Ok(response);
    }

    public async Task<Result<AclRuleResponse>> CreateAclRuleAsync(string userId, CreateAclRuleRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<AclRuleResponse>.Fail("Access token not found.");

        var body = new { role = request.Role, scope = new { type = request.ScopeType, value = request.ScopeValue } };
        var response = await _apiClient.PostAsync<AclRuleResponse>($"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/acl", body, accessToken);
        if (response == null) return Result<AclRuleResponse>.Fail("Failed to create ACL rule.");

        return Result<AclRuleResponse>.Ok(response);
    }

    public async Task<Result<AclRuleResponse>> UpdateAclRuleAsync(string userId, UpdateAclRuleRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<AclRuleResponse>.Fail("Access token not found.");

        var body = new { role = request.Role };
        var response = await _apiClient.PutAsync<AclRuleResponse>($"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/acl/{request.RuleId}", body, accessToken);
        if (response == null) return Result<AclRuleResponse>.Fail("Failed to update ACL rule.");

        return Result<AclRuleResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteAclRuleAsync(string userId, DeleteAclRuleRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/acl/{request.RuleId}", accessToken);
        return Result<bool>.Ok(true);
    }
}

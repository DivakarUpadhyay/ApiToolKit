using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ICalendarAclService
{
    Task<Result<ListAclResponse>> ListAclRulesAsync(string userId, string calendarId);
    Task<Result<AclRuleResponse>> GetAclRuleAsync(string userId, string calendarId, string ruleId);
    Task<Result<AclRuleResponse>> CreateAclRuleAsync(string userId, CreateAclRuleRequest request);
    Task<Result<AclRuleResponse>> UpdateAclRuleAsync(string userId, UpdateAclRuleRequest request);
    Task<Result<bool>> DeleteAclRuleAsync(string userId, DeleteAclRuleRequest request);
}

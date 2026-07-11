using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Calendar;

public class AclScope
{
    public string? Type { get; set; }
    public string? Value { get; set; }
}

public class AclRuleResponse
{
    public string? Id { get; set; }
    public string? Role { get; set; }
    public AclScope? Scope { get; set; }
    public string? Kind { get; set; }
    public string? Etag { get; set; }
}

public class ListAclResponse
{
    public List<AclRuleResponse>? Rules { get; set; }
    public string? NextPageToken { get; set; }
    public string? Etag { get; set; }
}

public class CreateAclRuleRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    [Required]
    public string ScopeType { get; set; } = string.Empty;
    public string? ScopeValue { get; set; }
}

public class UpdateAclRuleRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string RuleId { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
}

public class DeleteAclRuleRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string RuleId { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Gmail;

public class VacationSettingsResponse
{
    public bool EnableAutoReply { get; set; }
    public string? ResponseSubject { get; set; }
    public string? ResponseBodyPlainText { get; set; }
    public string? ResponseBodyHtml { get; set; }
    public long? StartTime { get; set; }
    public long? EndTime { get; set; }
    public bool RestrictToContacts { get; set; }
    public bool RestrictToDomain { get; set; }
}

public class UpdateVacationRequest
{
    public bool EnableAutoReply { get; set; }
    public string? ResponseSubject { get; set; }
    public string? ResponseBodyPlainText { get; set; }
    public string? ResponseBodyHtml { get; set; }
    public long? StartTime { get; set; }
    public long? EndTime { get; set; }
}

public class FilterCriteria
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Query { get; set; }
    public string? NegatedQuery { get; set; }
    public bool HasAttachment { get; set; }
}

public class FilterAction
{
    public List<string>? AddLabelIds { get; set; }
    public List<string>? RemoveLabelIds { get; set; }
    public string? Forward { get; set; }
}

public class FilterResponse
{
    public string? Id { get; set; }
    public FilterCriteria? Criteria { get; set; }
    public FilterAction? Action { get; set; }
}

public class CreateFilterRequest
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Query { get; set; }
    public bool HasAttachment { get; set; }
    public List<string>? AddLabelIds { get; set; }
    public List<string>? RemoveLabelIds { get; set; }
    public string? Forward { get; set; }
}

public class ForwardingAddressResponse
{
    public string? ForwardingEmail { get; set; }
    public string? VerificationStatus { get; set; }
}

public class CreateForwardingRequest
{
    [Required]
    public string ForwardingEmail { get; set; } = string.Empty;
}

public class ImapSettings
{
    public bool Enabled { get; set; }
    public bool AutoExpunge { get; set; }
    public string? ExpungeBehavior { get; set; }
    public int MaxFolderSize { get; set; }
}

public class PopSettings
{
    public string? AccessWindow { get; set; }
    public string? Disposition { get; set; }
}

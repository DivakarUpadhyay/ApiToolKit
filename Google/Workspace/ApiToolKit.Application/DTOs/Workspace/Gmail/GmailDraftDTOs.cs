using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Gmail;

public class DraftMessage
{
    public string? Id { get; set; }
    public string? ThreadId { get; set; }
    public List<string>? LabelIds { get; set; }
    public string? Snippet { get; set; }
}

public class DraftResponse
{
    public string? Id { get; set; }
    public DraftMessage? Message { get; set; }
}

public class ListDraftsResponse
{
    public List<DraftResponse>? Drafts { get; set; }
    public string? NextPageToken { get; set; }
    public int ResultSizeEstimate { get; set; }
}

public class CreateDraftRequest
{
    [Required]
    public List<string> To { get; set; } = new();

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;
    public string? HtmlBody { get; set; }
}

public class UpdateDraftRequest
{
    [Required]
    public string DraftId { get; set; } = string.Empty;
    public List<string>? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? HtmlBody { get; set; }
}

public class SendDraftRequest
{
    [Required]
    public string DraftId { get; set; } = string.Empty;
}

public class DeleteDraftRequest
{
    [Required]
    public string DraftId { get; set; } = string.Empty;
}

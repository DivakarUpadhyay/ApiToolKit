using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Gmail;

public class ThreadResponse
{
    public string? Id { get; set; }
    public string? Snippet { get; set; }
    public string? HistoryId { get; set; }
    public List<GmailMessageResponse>? Messages { get; set; }
}

public class ThreadRef
{
    public string? Id { get; set; }
    public string? Snippet { get; set; }
    public string? HistoryId { get; set; }
}

public class ListThreadsResponse
{
    public List<ThreadRef>? Threads { get; set; }
    public string? NextPageToken { get; set; }
    public int ResultSizeEstimate { get; set; }
}

public class ListThreadsRequest
{
    public string UserId { get; set; } = "me";
    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Q { get; set; }
    public List<string>? LabelIds { get; set; }
}

public class ModifyThreadRequest
{
    [Required]
    public string ThreadId { get; set; } = string.Empty;
    public List<string>? AddLabelIds { get; set; }
    public List<string>? RemoveLabelIds { get; set; }
}

public class DeleteThreadRequest
{
    [Required]
    public string ThreadId { get; set; } = string.Empty;
}

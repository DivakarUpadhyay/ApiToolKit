using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Gmail;

public class ListMessagesRequest
{
    public string UserId { get; set; } = "me";
    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Q { get; set; }
    public List<string>? LabelIds { get; set; }
    public bool IncludeSpamTrash { get; set; } = false;
}

public class MessagePartHeader
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class MessagePartBody
{
    public int Size { get; set; }
    public string? Data { get; set; }
    public string? AttachmentId { get; set; }
}

public class MessagePart
{
    public string? PartId { get; set; }
    public string? MimeType { get; set; }
    public List<MessagePartHeader>? Headers { get; set; }
    public MessagePartBody? Body { get; set; }
    public List<MessagePart>? Parts { get; set; }
}

public class GmailMessageResponse
{
    public string? Id { get; set; }
    public string? ThreadId { get; set; }
    public List<string>? LabelIds { get; set; }
    public string? Snippet { get; set; }
    public string? HistoryId { get; set; }
    public long InternalDate { get; set; }
    public int SizeEstimate { get; set; }
    public MessagePart? Payload { get; set; }
}

public class MessageRef
{
    public string? Id { get; set; }
    public string? ThreadId { get; set; }
}

public class ListMessagesResponse
{
    public List<MessageRef>? Messages { get; set; }
    public string? NextPageToken { get; set; }
    public int ResultSizeEstimate { get; set; }
}

public class SendMessageRequest
{
    [Required]
    public List<string> To { get; set; } = new();
    public List<string>? Cc { get; set; }
    public List<string>? Bcc { get; set; }

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;
    public string? HtmlBody { get; set; }
    public string? ReplyTo { get; set; }
    public string? InReplyTo { get; set; }
    public string? References { get; set; }
}

public class ModifyMessageRequest
{
    [Required]
    public string MessageId { get; set; } = string.Empty;
    public string UserId { get; set; } = "me";
    public List<string>? AddLabelIds { get; set; }
    public List<string>? RemoveLabelIds { get; set; }
}

public class BatchModifyRequest
{
    [Required]
    public List<string> Ids { get; set; } = new();
    public List<string>? AddLabelIds { get; set; }
    public List<string>? RemoveLabelIds { get; set; }
}

public class DeleteMessageRequest
{
    [Required]
    public string MessageId { get; set; } = string.Empty;
    public string UserId { get; set; } = "me";
}

public class TrashMessageRequest
{
    [Required]
    public string MessageId { get; set; } = string.Empty;
    public string UserId { get; set; } = "me";
}

public class GetAttachmentRequest
{
    [Required]
    public string MessageId { get; set; } = string.Empty;

    [Required]
    public string AttachmentId { get; set; } = string.Empty;
    public string UserId { get; set; } = "me";
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Messenger;

public class ButtonTemplate
{
    public string Type { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? Payload { get; set; }
}

public class QuickReply
{
    public string ContentType { get; set; } = "text";
    public string? Title { get; set; }
    public string? Payload { get; set; }
    public string? ImageUrl { get; set; }
}

public class SendMessageRequest
{
    [Required] public string RecipientId { get; set; } = string.Empty;
    public string? MessageText { get; set; }
    public List<QuickReply>? QuickReplies { get; set; }
    public List<ButtonTemplate>? Buttons { get; set; }
    public string? AttachmentType { get; set; }
    public string? AttachmentUrl { get; set; }
    public string? TemplateType { get; set; }
}

public class SendMessageResponse
{
    public string RecipientId { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
}

public class GetConversationsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class ConversationParticipant
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class ConversationResponse
{
    public string Id { get; set; } = string.Empty;
    public List<ConversationParticipant> ParticipantsList { get; set; } = new();
    public int MessageCount { get; set; }
    public string? UpdatedTime { get; set; }
    public int UnreadCount { get; set; }
}

public class GetMessagesRequest
{
    [Required] public string ConversationId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class MessageResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Message { get; set; }
    public object? From { get; set; }
    public object? To { get; set; }
    public string? CreatedTime { get; set; }
}

public class GetUserProfileRequest
{
    [Required] public string Psid { get; set; } = string.Empty;
}

public class MessengerUserProfile
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePic { get; set; }
    public string? Locale { get; set; }
    public double? Timezone { get; set; }
    public string? Gender { get; set; }
}

public class MarkSeenRequest
{
    [Required] public string RecipientId { get; set; } = string.Empty;
}

public class TypingOnRequest
{
    [Required] public string RecipientId { get; set; } = string.Empty;
}

public class SubscribeAppRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public List<string> SubscribedFields { get; set; } = new();
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Chat;

public class SpaceDetails
{
    public string? Description { get; set; }
    public List<string>? Guidelines { get; set; }
}

public class ChatSpaceResponse
{
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public string? SpaceType { get; set; }
    public string? SpaceUri { get; set; }
    public bool AdminInstalled { get; set; }
    public int MembershipCount { get; set; }
    public SpaceDetails? SpaceDetails { get; set; }
}

public class ListChatSpacesResponse
{
    public List<ChatSpaceResponse>? Spaces { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreateChatSpaceRequest
{
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    public string SpaceType { get; set; } = "SPACE";
    public List<string>? Members { get; set; }
}

public class ChatSender
{
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public string? Type { get; set; }
}

public class MessageThread
{
    public string? Name { get; set; }
}

public class MessageSpace
{
    public string? Name { get; set; }
}

public class MessageResponse
{
    public string? Name { get; set; }
    public string? Text { get; set; }
    public string? FormattedText { get; set; }
    public string? CreateTime { get; set; }
    public string? LastUpdateTime { get; set; }
    public MessageThread? Thread { get; set; }
    public MessageSpace? Space { get; set; }
    public ChatSender? Sender { get; set; }
    public object? Annotations { get; set; }
    public object? Cards { get; set; }
}

public class ListMessagesRequest
{
    [Required]
    public string SpaceName { get; set; } = string.Empty;
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Filter { get; set; }
    public string OrderBy { get; set; } = "createTime ASC";
    public bool ShowDeleted { get; set; } = false;
}

public class ListMessagesResponse
{
    public List<MessageResponse>? Messages { get; set; }
    public string? NextPageToken { get; set; }
}

public class SendMessageRequest
{
    [Required]
    public string SpaceName { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
    public string? ThreadName { get; set; }
    public string ReplyOption { get; set; } = "REPLY_MESSAGE_FALLBACK_TO_NEW_THREAD";
}

public class UpdateMessageRequest
{
    [Required]
    public string MessageName { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
    public string UpdateMask { get; set; } = "text";
}

public class DeleteMessageRequest
{
    [Required]
    public string MessageName { get; set; } = string.Empty;
}

public class MemberInfo
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? DisplayName { get; set; }
}

public class MemberResponse
{
    public string? Name { get; set; }
    public string? State { get; set; }
    public string? Role { get; set; }
    public string? CreateTime { get; set; }
    public string? DeleteTime { get; set; }
    public MemberInfo? Member { get; set; }
}

public class ListMembersRequest
{
    [Required]
    public string SpaceName { get; set; } = string.Empty;
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Filter { get; set; }
}

public class ListMembersResponse
{
    public List<MemberResponse>? Memberships { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreateMemberRequest
{
    [Required]
    public string SpaceName { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty;
}

public class DeleteMemberRequest
{
    [Required]
    public string MembershipName { get; set; } = string.Empty;
}

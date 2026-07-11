using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Group;

public class GroupResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Privacy { get; set; }
    public string? Description { get; set; }
    public int MemberCount { get; set; }
    public int MemberRequestCount { get; set; }
}

public class GroupFeedRequest
{
    [Required] public string GroupId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class GroupMembersRequest
{
    [Required] public string GroupId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class GroupMemberResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Administrator { get; set; }
    public string? Picture { get; set; }
}

public class CreateGroupPostRequest
{
    [Required] public string GroupId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Link { get; set; }
}

public class GroupAlbumsRequest
{
    [Required] public string GroupId { get; set; } = string.Empty;
}

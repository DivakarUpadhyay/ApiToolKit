namespace ApiToolKit.Application.DTOs.Facebook.User;

public class UserProfileResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Picture { get; set; }
    public string? Birthday { get; set; }
    public string? Gender { get; set; }
    public string? Location { get; set; }
    public string? About { get; set; }
    public string? Link { get; set; }
}

public class UserFriendItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class UserFriendsResponse
{
    public List<UserFriendItem> Data { get; set; } = new();
    public int Total { get; set; }
}

public class UserPermissionItem
{
    public string Permission { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class UserPermissionsResponse
{
    public List<UserPermissionItem> Data { get; set; } = new();
}

public class UserPictureResponse
{
    public bool IsSilhouette { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
}

public class UserFeedResponse
{
    public List<object> Data { get; set; } = new();
}

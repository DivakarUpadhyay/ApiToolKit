namespace ApiToolKit.Domain.Entities.Identity;

public class ContactGroup
{
    public int Id { get; set; }
    public string GroupResourceName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? FormattedName { get; set; }
    public string? GroupType { get; set; }
    public int MemberCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

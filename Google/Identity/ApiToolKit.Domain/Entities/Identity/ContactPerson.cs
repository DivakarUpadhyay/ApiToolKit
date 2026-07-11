namespace ApiToolKit.Domain.Entities.Identity;

public class ContactPerson
{
    public int Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Organization { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }
    public string? Etag { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

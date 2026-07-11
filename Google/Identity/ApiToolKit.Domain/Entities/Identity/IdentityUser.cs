namespace ApiToolKit.Domain.Entities.Identity;

public class IdentityUser
{
    public int Id { get; set; }
    public string LocalId { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public bool EmailVerified { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProviderUserInfo { get; set; }
    public long CreatedAt { get; set; }
    public long LastLoginAt { get; set; }
    public string? LastRefreshAt { get; set; }
    public bool Disabled { get; set; }
    public DateTime LocalCreatedAt { get; set; }
}

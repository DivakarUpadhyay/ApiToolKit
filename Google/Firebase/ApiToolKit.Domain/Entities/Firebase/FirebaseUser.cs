namespace ApiToolKit.Domain.Entities.Firebase;

public class FirebaseUser
{
    public int Id { get; set; }
    public string Uid { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool EmailVerified { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public bool Disabled { get; set; }
    public string? CreationTime { get; set; }
    public string? LastSignInTime { get; set; }
    public string? ProviderData { get; set; }
    public DateTime CreatedAt { get; set; }
}

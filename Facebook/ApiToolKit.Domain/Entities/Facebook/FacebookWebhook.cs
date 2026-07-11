namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookWebhook
{
    public int Id { get; set; }
    public string ObjectType { get; set; } = string.Empty;
    public string Fields { get; set; } = string.Empty;
    public string CallbackUrl { get; set; } = string.Empty;
    public string VerifyToken { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

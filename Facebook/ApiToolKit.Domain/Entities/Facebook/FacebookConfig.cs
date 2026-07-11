namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookConfig
{
    public int Id { get; set; }
    public string AppId { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookAdAccount
{
    public int Id { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? TimezoneName { get; set; }
    public string? Balance { get; set; }
    public int Status { get; set; }
    public string? BusinessId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

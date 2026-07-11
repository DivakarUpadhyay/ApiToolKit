namespace ApiToolKit.Domain.Entities.Identity;

public class IdentityApiLog
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

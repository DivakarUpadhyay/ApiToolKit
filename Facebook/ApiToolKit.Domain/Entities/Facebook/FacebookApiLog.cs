namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookApiLog
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? PageId { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

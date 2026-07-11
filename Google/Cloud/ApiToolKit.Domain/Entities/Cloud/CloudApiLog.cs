namespace ApiToolKit.Domain.Entities.Cloud;

public class CloudApiLog
{
    public long Id { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty; // Vision / Speech / Language / Translation
    public string ApiName { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

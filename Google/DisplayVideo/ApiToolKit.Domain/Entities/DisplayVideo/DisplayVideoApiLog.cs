namespace ApiToolKit.Domain.Entities.DisplayVideo;

public class DisplayVideoApiLog
{
    public long Id { get; set; }
    public string? AccountKey { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

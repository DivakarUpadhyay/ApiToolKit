namespace ApiToolKit.Domain.Entities.Maps;

public class MapsApiLog
{
    public long Id { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string? RequestParams { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsApiLog
{
    public int Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string ApiName { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

namespace ApiToolKit.Domain.Entities.Shopee;

public class ShopeeApiLog
{
    public int Id { get; set; }
    public long ShopId { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

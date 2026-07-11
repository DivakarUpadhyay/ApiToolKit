namespace ApiToolKit.Domain.Entities.Search;

public class SearchApiLog
{
    public int Id { get; set; }
    public string ApiName { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public string? ResponseData { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

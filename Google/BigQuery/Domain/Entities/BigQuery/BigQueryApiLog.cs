namespace ApiToolKit.Domain.Entities.BigQuery;

public class BigQueryApiLog
{
    public int Id { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string ApiName { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

namespace ApiToolKit.Domain.Entities.BigQuery;

public class BigQueryTableInfo
{
    public int Id { get; set; }
    public string TableId { get; set; } = string.Empty;
    public string DatasetId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public long NumRows { get; set; }
    public long NumBytes { get; set; }
    public long CreatedTime { get; set; }
    public long LastModifiedTime { get; set; }
    public DateTime LocalCreatedAt { get; set; }
}

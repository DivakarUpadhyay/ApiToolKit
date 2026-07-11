namespace ApiToolKit.Domain.Entities.BigQuery;

public class BigQueryDataset
{
    public int Id { get; set; }
    public string DatasetId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public long CreatedAt { get; set; }
    public long LastModifiedTime { get; set; }
    public DateTime LocalCreatedAt { get; set; }
}

namespace ApiToolKit.Domain.Entities.BigQuery;

public class BigQueryJob
{
    public int Id { get; set; }
    public string JobId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? JobType { get; set; }
    public string? Status { get; set; }
    public long CreationTime { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long TotalRows { get; set; }
    public long TotalBytesProcessed { get; set; }
    public string? Query { get; set; }
    public DateTime LocalCreatedAt { get; set; }
}

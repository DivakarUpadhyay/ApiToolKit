using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BigQuery.Table;

public class TableReference
{
    public string? ProjectId { get; set; }
    public string? DatasetId { get; set; }
    public string? TableId { get; set; }
}

public class TableFieldSchema
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    public string Mode { get; set; } = "NULLABLE";
    public string? Description { get; set; }
    public List<TableFieldSchema>? Fields { get; set; }
    public object? PolicyTags { get; set; }
    public long? MaxLength { get; set; }
    public long? Precision { get; set; }
    public long? Scale { get; set; }
    public string? DefaultValueExpression { get; set; }
}

public class TableSchema
{
    [Required]
    public List<TableFieldSchema> Fields { get; set; } = new();
}

public class TimePartitioning
{
    public string Type { get; set; } = "DAY";
    public string? Field { get; set; }
    public long? ExpirationMs { get; set; }
    public bool RequirePartitionFilter { get; set; }
}

public class RangePartitioningRange
{
    public long Start { get; set; }
    public long End { get; set; }
    public long Interval { get; set; }
}

public class RangePartitioning
{
    public string? Field { get; set; }
    public RangePartitioningRange? Range { get; set; }
}

public class Clustering
{
    public List<string>? Fields { get; set; }
}

public class EncryptionConfiguration
{
    public string? KmsKeyName { get; set; }
}

public class TableViewDefinition
{
    public string? Query { get; set; }
    public bool UseLegacySql { get; set; }
}

public class MaterializedViewDefinition
{
    public string? Query { get; set; }
    public bool EnableRefresh { get; set; }
    public long RefreshIntervalMs { get; set; }
}

public class TableResponse
{
    public string? Id { get; set; }
    public TableReference? TableReference { get; set; }
    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public TableSchema? Schema { get; set; }
    public long NumRows { get; set; }
    public long NumBytes { get; set; }
    public long NumLongTermBytes { get; set; }
    public string? Type { get; set; }
    public long CreationTime { get; set; }
    public long LastModifiedTime { get; set; }
    public string? Location { get; set; }
    public TimePartitioning? TimePartitioning { get; set; }
    public Clustering? Clustering { get; set; }
    public string? Etag { get; set; }
    public string? SelfLink { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
    public long? ExpirationTime { get; set; }
    public bool RequirePartitionFilter { get; set; }
    public MaterializedViewDefinition? MaterializedView { get; set; }
}

public class ListTablesRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
}

public class ListTablesResponse
{
    public List<TableResponse>? Tables { get; set; }
    public string? NextPageToken { get; set; }
    public int TotalItems { get; set; }
}

public class CreateTableRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string TableId { get; set; } = string.Empty;

    public string? FriendlyName { get; set; }
    public string? Description { get; set; }

    [Required]
    public TableSchema Schema { get; set; } = new();

    public TimePartitioning? TimePartitioning { get; set; }
    public RangePartitioning? RangePartitioning { get; set; }
    public Clustering? Clustering { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
    public long? ExpirationTime { get; set; }
    public bool RequirePartitionFilter { get; set; } = false;
    public EncryptionConfiguration? EncryptionConfiguration { get; set; }
    public TableViewDefinition? View { get; set; }
    public MaterializedViewDefinition? MaterializedView { get; set; }
}

public class UpdateTableRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string TableId { get; set; } = string.Empty;

    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public TableSchema? Schema { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
    public long? ExpirationTime { get; set; }
    public bool? RequirePartitionFilter { get; set; }
}

public class DeleteTableRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string TableId { get; set; } = string.Empty;
}

public class CopyTableRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string SourceDatasetId { get; set; } = string.Empty;

    [Required]
    public string SourceTableId { get; set; } = string.Empty;

    [Required]
    public string DestDatasetId { get; set; } = string.Empty;

    [Required]
    public string DestTableId { get; set; } = string.Empty;

    public string WriteDisposition { get; set; } = "WRITE_EMPTY";
    public string CreateDisposition { get; set; } = "CREATE_IF_NEEDED";
}

using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.BigQuery.Table;

namespace ApiToolKit.Application.DTOs.BigQuery.Job;

public class QueryParameterType
{
    public string? Type { get; set; }
    public QueryParameterType? ArrayType { get; set; }
    public List<object>? StructTypes { get; set; }
}

public class QueryParameterValue
{
    public string? Value { get; set; }
    public List<QueryParameterValue>? ArrayValues { get; set; }
    public Dictionary<string, QueryParameterValue>? StructValues { get; set; }
}

public class QueryParameter
{
    public string? Name { get; set; }
    public QueryParameterType? ParameterType { get; set; }
    public QueryParameterValue? ParameterValue { get; set; }
}

public class DefaultDataset
{
    public string? ProjectId { get; set; }
    public string? DatasetId { get; set; }
}

public class QueryRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Query { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 1000;
    public DefaultDataset? DefaultDataset { get; set; }
    public string Location { get; set; } = "US";
    public bool UseLegacySql { get; set; } = false;
    public List<QueryParameter>? QueryParameters { get; set; }
    public int TimeoutMs { get; set; } = 30000;
    public bool CreateSession { get; set; } = false;
}

public class TableRowCell
{
    public object? V { get; set; }
}

public class TableRow
{
    public List<TableRowCell>? F { get; set; }
}

public class QueryResponseSchema
{
    public List<TableFieldSchema>? Fields { get; set; }
}

public class JobReference
{
    public string? ProjectId { get; set; }
    public string? JobId { get; set; }
    public string? Location { get; set; }
}

public class QueryResponse
{
    public bool JobComplete { get; set; }
    public JobReference? JobReference { get; set; }
    public QueryResponseSchema? Schema { get; set; }
    public List<TableRow>? Rows { get; set; }
    public long TotalRows { get; set; }
    public long TotalBytesProcessed { get; set; }
    public string? PageToken { get; set; }
    public long? NumDmlAffectedRows { get; set; }
    public object? DmlStats { get; set; }
}

public class InsertAllRow
{
    public string? InsertId { get; set; }
    public Dictionary<string, object>? Json { get; set; }
}

public class InsertAllRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string TableId { get; set; } = string.Empty;

    [Required]
    public List<InsertAllRow> Rows { get; set; } = new();

    public bool SkipInvalidRows { get; set; } = false;
    public bool IgnoreUnknownValues { get; set; } = false;
    public string? TemplateSuffix { get; set; }
}

public class InsertError
{
    public string? Reason { get; set; }
    public string? Location { get; set; }
    public string? DebugInfo { get; set; }
    public string? Message { get; set; }
}

public class InsertErrorEntry
{
    public int Index { get; set; }
    public List<InsertError>? Errors { get; set; }
}

public class InsertAllResponse
{
    public List<InsertErrorEntry>? InsertErrors { get; set; }
}

public class ListRowsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string TableId { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 1000;
    public string? PageToken { get; set; }
    public string? SelectedFields { get; set; }
}

public class ListRowsResponse
{
    public QueryResponseSchema? Schema { get; set; }
    public List<TableRow>? Rows { get; set; }
    public string? PageToken { get; set; }
    public long TotalRows { get; set; }
    public string? Etag { get; set; }
}

public class JobConfigurationQuery
{
    [Required]
    public string Query { get; set; } = string.Empty;

    public DefaultDataset? DefaultDataset { get; set; }
    public string CreateDisposition { get; set; } = "CREATE_IF_NEEDED";
    public string WriteDisposition { get; set; } = "WRITE_TRUNCATE";
    public TableReference? DestinationTable { get; set; }
    public bool UseLegacySql { get; set; } = false;
    public List<QueryParameter>? QueryParameters { get; set; }
    public bool AllowLargeResults { get; set; } = false;
    public bool FlattenResults { get; set; } = true;
    public string Priority { get; set; } = "INTERACTIVE";
    public string ParameterMode { get; set; } = "NAMED";
}

public class JobConfigurationLoad
{
    [Required]
    public List<string> SourceUris { get; set; } = new();

    [Required]
    public TableReference DestinationTable { get; set; } = new();

    public TableSchema? Schema { get; set; }
    public string SourceFormat { get; set; } = "CSV";
    public int SkipLeadingRows { get; set; } = 0;
    public string FieldDelimiter { get; set; } = ",";
    public string WriteDisposition { get; set; } = "WRITE_TRUNCATE";
    public string CreateDisposition { get; set; } = "CREATE_IF_NEEDED";
    public bool AllowJaggedRows { get; set; } = false;
    public bool AllowQuotedNewlines { get; set; } = false;
    public bool IgnoreUnknownValues { get; set; } = false;
    public int MaxBadRecords { get; set; } = 0;
    public string Encoding { get; set; } = "UTF-8";
}

public class JobConfigurationExtract
{
    [Required]
    public TableReference SourceTable { get; set; } = new();

    [Required]
    public List<string> DestinationUris { get; set; } = new();

    public string DestinationFormat { get; set; } = "CSV";
    public string FieldDelimiter { get; set; } = ",";
    public bool PrintHeader { get; set; } = true;
    public string Compression { get; set; } = "NONE";
}

public class CreateJobRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public string? JobId { get; set; }
    public string Location { get; set; } = "US";
    public JobConfigurationQuery? QueryConfig { get; set; }
    public JobConfigurationLoad? LoadConfig { get; set; }
    public JobConfigurationExtract? ExtractConfig { get; set; }
}

public class JobErrorResult
{
    public string? Reason { get; set; }
    public string? Location { get; set; }
    public string? DebugInfo { get; set; }
    public string? Message { get; set; }
}

public class JobStatus
{
    public string? State { get; set; }
    public JobErrorResult? ErrorResult { get; set; }
    public List<JobErrorResult>? Errors { get; set; }
}

public class JobQueryStatistics
{
    public long TotalBytesProcessed { get; set; }
    public long TotalSlotMs { get; set; }
    public bool CacheHit { get; set; }
    public string? StatementType { get; set; }
    public List<TableReference>? ReferencedTables { get; set; }
}

public class JobStatistics
{
    public long CreationTime { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long TotalBytesProcessed { get; set; }
    public long TotalSlotMs { get; set; }
    public JobQueryStatistics? Query { get; set; }
}

public class JobResponse
{
    public string? Id { get; set; }
    public JobReference? JobReference { get; set; }
    public JobStatus? Status { get; set; }
    public object? Configuration { get; set; }
    public JobStatistics? Statistics { get; set; }
    public string? SelfLink { get; set; }
    public string? UserEmail { get; set; }
    public string? Kind { get; set; }
}

public class ListJobsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
    public bool AllUsers { get; set; } = false;
    public string Projection { get; set; } = "full";
    public string? StateFilter { get; set; }
    public long? MinCreationTime { get; set; }
    public long? MaxCreationTime { get; set; }
    public string? ParentJobId { get; set; }
}

public class ListJobsResponse
{
    public List<JobResponse>? Jobs { get; set; }
    public string? NextPageToken { get; set; }
    public string? Etag { get; set; }
}

public class CancelJobRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string JobId { get; set; } = string.Empty;

    public string? Location { get; set; }
}

public class GetJobRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string JobId { get; set; } = string.Empty;

    public string? Location { get; set; }
}

// TableReference is defined in ApiToolKit.Application.DTOs.BigQuery.Table namespace
// and imported via the using statement above.

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BigQuery.Dataset;

public class DatasetReference
{
    public string? DatasetId { get; set; }
    public string? ProjectId { get; set; }
}

public class DatasetAccess
{
    public string? Role { get; set; }
    public string? UserByEmail { get; set; }
    public string? GroupByEmail { get; set; }
    public string? Domain { get; set; }
    public string? SpecialGroup { get; set; }
    public string? IamMember { get; set; }
    public object? Routine { get; set; }
    public object? Dataset { get; set; }
    public object? View { get; set; }
}

public class DatasetResponse
{
    public string? Id { get; set; }
    public DatasetReference? DatasetReference { get; set; }
    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public long CreationTime { get; set; }
    public long LastModifiedTime { get; set; }
    public List<DatasetAccess>? Access { get; set; }
    public string? Etag { get; set; }
    public string? SelfLink { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
    public long? DefaultTableExpirationMs { get; set; }
    public long? DefaultPartitionExpirationMs { get; set; }
    public object? DefaultEncryptionConfiguration { get; set; }
    public bool IsCaseInsensitive { get; set; }
}

public class ListDatasetsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
    public bool All { get; set; } = false;
    public string? Filter { get; set; }
    public string? Labels { get; set; }
}

public class ListDatasetsResponse
{
    public List<DatasetResponse>? Datasets { get; set; }
    public string? NextPageToken { get; set; }
    public string? Etag { get; set; }
}

public class CreateDatasetRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public string Location { get; set; } = "US";
    public long? DefaultTableExpirationMs { get; set; } = null;
    public Dictionary<string, string>? Labels { get; set; }
}

public class UpdateDatasetRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    public string? FriendlyName { get; set; }
    public string? Description { get; set; }
    public long? DefaultTableExpirationMs { get; set; }
    public List<DatasetAccess>? Access { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
}

public class DeleteDatasetRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    public bool DeleteContents { get; set; } = false;
}

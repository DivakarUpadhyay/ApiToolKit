using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BigQuery.Routine;

public class DataTypeDto
{
    public string? TypeKind { get; set; }
    public DataTypeDto? ArrayElementType { get; set; }
    public object? StructType { get; set; }
}

public class ArgumentDto
{
    public string? Name { get; set; }
    public string ArgumentKind { get; set; } = "FIXED_TYPE";
    public string Mode { get; set; } = "IN";
    public DataTypeDto? DataType { get; set; }
}

public class RoutineReference
{
    public string? ProjectId { get; set; }
    public string? DatasetId { get; set; }
    public string? RoutineId { get; set; }
}

public class RoutineResponse
{
    public RoutineReference? RoutineReference { get; set; }
    public string? RoutineType { get; set; }
    public string? Language { get; set; }
    public List<ArgumentDto>? Arguments { get; set; }
    public DataTypeDto? ReturnType { get; set; }

    [Required]
    public string DefinitionBody { get; set; } = string.Empty;

    public string? Description { get; set; }
    public long CreationTime { get; set; }
    public long LastModifiedTime { get; set; }
    public string? Etag { get; set; }
}

public class ListRoutinesRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    public int MaxResults { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Filter { get; set; }
}

public class ListRoutinesResponse
{
    public List<RoutineResponse>? Routines { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreateRoutineRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string RoutineId { get; set; } = string.Empty;

    public string RoutineType { get; set; } = "SCALAR_FUNCTION";
    public string Language { get; set; } = "SQL";
    public List<ArgumentDto>? Arguments { get; set; }
    public DataTypeDto? ReturnType { get; set; }

    [Required]
    public string DefinitionBody { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class DeleteRoutineRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string DatasetId { get; set; } = string.Empty;

    [Required]
    public string RoutineId { get; set; } = string.Empty;
}

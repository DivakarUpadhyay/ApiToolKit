namespace ApiToolKit.Application.DTOs.Analytics.Metadata;

public class DimensionMetadata
{
    public string ApiName { get; set; } = string.Empty;
    public string UiName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> DeprecatedApiNames { get; set; } = new();
    public bool CustomDefinition { get; set; }
    public string? Category { get; set; }
}

public class MetricMetadata
{
    public string ApiName { get; set; } = string.Empty;
    public string UiName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Expression { get; set; }
    public bool CustomDefinition { get; set; }
    public List<string> BlockedReasons { get; set; } = new();
    public string? Category { get; set; }
}

public class MetadataResponse
{
    public string Name { get; set; } = string.Empty;
    public List<DimensionMetadata> Dimensions { get; set; } = new();
    public List<MetricMetadata> Metrics { get; set; } = new();
}

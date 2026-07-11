namespace ApiToolKit.Application.DTOs.Analytics.Property;

public class PropertyResponse
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? TimeZone { get; set; }
    public string? CurrencyCode { get; set; }
    public string? IndustryCategory { get; set; }
    public string? CreateTime { get; set; }
    public string? UpdateTime { get; set; }
    public string? ServiceLevel { get; set; }
    public string? AccountName { get; set; }
    public string? PropertyType { get; set; }
    public string? Parent { get; set; }
}

public class ListPropertiesResponse
{
    public List<PropertyResponse> Properties { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class AccountResponse
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? RegionCode { get; set; }
    public string? CreateTime { get; set; }
    public string? UpdateTime { get; set; }
}

public class ListAccountsResponse
{
    public List<AccountResponse> Accounts { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class DataStreamResponse
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? CreateTime { get; set; }
    public string? UpdateTime { get; set; }
    public object? WebStreamData { get; set; }
    public object? AndroidAppStreamData { get; set; }
    public object? IosAppStreamData { get; set; }
}

public class ListDataStreamsResponse
{
    public List<DataStreamResponse> DataStreams { get; set; } = new();
    public string? NextPageToken { get; set; }
}

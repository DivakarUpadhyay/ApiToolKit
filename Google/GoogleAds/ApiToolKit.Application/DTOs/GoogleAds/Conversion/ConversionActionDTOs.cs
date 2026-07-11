using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Conversion;

public class ListConversionActionsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
}

public class ConversionActionResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string CountingType { get; set; } = string.Empty;
    public object? ValueSettings { get; set; }
}

public class CreateConversionActionRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "WEBPAGE";
    public string Category { get; set; } = "PURCHASE";
    public string CountingType { get; set; } = "MANY_PER_CLICK";
}

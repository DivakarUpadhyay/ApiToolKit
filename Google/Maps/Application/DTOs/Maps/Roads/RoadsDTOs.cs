using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Maps.Geocoding;

namespace ApiToolKit.Application.DTOs.Maps.Roads;

public class SnapToRoadsRequest
{
    [Required]
    public List<string> Path { get; set; } = new();
    public bool Interpolate { get; set; } = true;
}

public class SnappedPoint
{
    public GeocodeLocation Location { get; set; } = new();
    public int? OriginalIndex { get; set; }
    public string PlaceId { get; set; } = string.Empty;
}

public class SnapToRoadsResponse
{
    public List<SnappedPoint> SnappedPoints { get; set; } = new();
    public string? WarningMessage { get; set; }
}

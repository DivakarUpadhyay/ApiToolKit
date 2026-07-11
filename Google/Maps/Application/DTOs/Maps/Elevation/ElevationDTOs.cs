using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Maps.Geocoding;

namespace ApiToolKit.Application.DTOs.Maps.Elevation;

public class ElevationRequest
{
    [Required]
    public List<string> Locations { get; set; } = new();
}

public class ElevationPathRequest
{
    [Required]
    public List<string> Path { get; set; } = new();
    [Required]
    public int Samples { get; set; }
}

public class ElevationResult
{
    public double Elevation { get; set; }
    public GeocodeLocation Location { get; set; } = new();
    public double Resolution { get; set; }
}

public class ElevationResponse
{
    public string Status { get; set; } = string.Empty;
    public List<ElevationResult> Results { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

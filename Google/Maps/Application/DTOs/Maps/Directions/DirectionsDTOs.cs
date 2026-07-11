using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Maps.Geocoding;

namespace ApiToolKit.Application.DTOs.Maps.Directions;

public class DirectionsRequest
{
    [Required]
    public string Origin { get; set; } = string.Empty;
    [Required]
    public string Destination { get; set; } = string.Empty;
    public string Mode { get; set; } = "driving";
    public string Language { get; set; } = "en";
    public string Units { get; set; } = "metric";
    public string? Avoid { get; set; }
    public string? DepartureTime { get; set; }
    public string? ArrivalTime { get; set; }
    public bool Alternatives { get; set; } = false;
    public List<string> WaypointsList { get; set; } = new();
}

public class DistanceValue
{
    public int Value { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class PolylineValue
{
    public string Points { get; set; } = string.Empty;
}

public class DirectionsStep
{
    public DistanceValue Distance { get; set; } = new();
    public DistanceValue Duration { get; set; } = new();
    public GeocodeLocation StartLocation { get; set; } = new();
    public GeocodeLocation EndLocation { get; set; } = new();
    public string? HtmlInstructions { get; set; }
    public string? TravelMode { get; set; }
    public PolylineValue Polyline { get; set; } = new();
    public object? TransitDetails { get; set; }
}

public class DirectionsLeg
{
    public DistanceValue Distance { get; set; } = new();
    public DistanceValue Duration { get; set; } = new();
    public DistanceValue? DurationInTraffic { get; set; }
    public string StartAddress { get; set; } = string.Empty;
    public string EndAddress { get; set; } = string.Empty;
    public GeocodeLocation StartLocation { get; set; } = new();
    public GeocodeLocation EndLocation { get; set; } = new();
    public List<DirectionsStep> Steps { get; set; } = new();
}

public class DirectionsRoute
{
    public string Summary { get; set; } = string.Empty;
    public List<DirectionsLeg> Legs { get; set; } = new();
    public PolylineValue OverviewPolyline { get; set; } = new();
    public object? Bounds { get; set; }
    public string? Copyrights { get; set; }
    public List<string> Warnings { get; set; } = new();
}

public class DirectionsResponse
{
    public string Status { get; set; } = string.Empty;
    public List<DirectionsRoute> Routes { get; set; } = new();
    public object? GeocodedWaypoints { get; set; }
    public string? ErrorMessage { get; set; }
}

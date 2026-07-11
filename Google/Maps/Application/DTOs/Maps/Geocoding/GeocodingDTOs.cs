using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Maps.Geocoding;

public class GeocodeAddressRequest
{
    [Required]
    public string Address { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public string? Region { get; set; }
    public string? Components { get; set; }
    public string? Bounds { get; set; }
}

public class ReverseGeocodeRequest
{
    [Required]
    public double Lat { get; set; }
    [Required]
    public double Lng { get; set; }
    public string Language { get; set; } = "en";
    public string? ResultType { get; set; }
    public string? LocationType { get; set; }
}

public class AddressComponent
{
    public string LongName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public List<string> Types { get; set; } = new();
}

public class GeocodeLocation
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class GeocodeViewport
{
    public GeocodeLocation Northeast { get; set; } = new();
    public GeocodeLocation Southwest { get; set; } = new();
}

public class GeocodeGeometry
{
    public GeocodeLocation Location { get; set; } = new();
    public string? LocationType { get; set; }
    public GeocodeViewport Viewport { get; set; } = new();
    public GeocodeViewport? Bounds { get; set; }
}

public class GeocodeResult
{
    public string PlaceId { get; set; } = string.Empty;
    public string FormattedAddress { get; set; } = string.Empty;
    public List<AddressComponent> AddressComponents { get; set; } = new();
    public GeocodeGeometry Geometry { get; set; } = new();
    public List<string> Types { get; set; } = new();
    public object? PlusCode { get; set; }
}

public class GeocodeResponse
{
    public string Status { get; set; } = string.Empty;
    public List<GeocodeResult> Results { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

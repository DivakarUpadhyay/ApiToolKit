namespace ApiToolKit.Domain.Entities.Maps;

public class GeocodingResult
{
    public long Id { get; set; }
    public string PlaceId { get; set; } = string.Empty;
    public string FormattedAddress { get; set; } = string.Empty;
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string? LocationType { get; set; }
    public string? AddressComponents { get; set; }
    public DateTime CreatedAt { get; set; }
}

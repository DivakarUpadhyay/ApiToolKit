namespace ApiToolKit.Domain.Entities.Maps;

public class PlaceResult
{
    public long Id { get; set; }
    public string PlaceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? FormattedAddress { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public double Rating { get; set; }
    public int UserRatingsTotal { get; set; }
    public string? Types { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Website { get; set; }
    public string? BusinessStatus { get; set; }
    public bool? OpenNow { get; set; }
    public int? PriceLevel { get; set; }
    public DateTime CreatedAt { get; set; }
}

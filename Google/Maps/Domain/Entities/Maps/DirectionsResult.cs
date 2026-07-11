namespace ApiToolKit.Domain.Entities.Maps;

public class DirectionsResult
{
    public long Id { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public int Distance { get; set; }
    public int Duration { get; set; }
    public string? DistanceText { get; set; }
    public string? DurationText { get; set; }
    public string? Steps { get; set; }
    public string? EncodedPolyline { get; set; }
    public DateTime CreatedAt { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Maps.TimeZone;

public class TimeZoneRequest
{
    [Required]
    public double Lat { get; set; }
    [Required]
    public double Lng { get; set; }
    public long Timestamp { get; set; }
}

public class TimeZoneResponse
{
    public string Status { get; set; } = string.Empty;
    public string? TimeZoneId { get; set; }
    public string? TimeZoneName { get; set; }
    public int DstOffset { get; set; }
    public int RawOffset { get; set; }
    public string? ErrorMessage { get; set; }
}

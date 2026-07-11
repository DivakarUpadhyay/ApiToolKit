using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Maps.DistanceMatrix;

public class DistanceMatrixRequest
{
    [Required]
    public List<string> Origins { get; set; } = new();
    [Required]
    public List<string> Destinations { get; set; } = new();
    public string Mode { get; set; } = "driving";
    public string Language { get; set; } = "en";
    public string Units { get; set; } = "metric";
    public string? Avoid { get; set; }
    public string? DepartureTime { get; set; }
    public string? TrafficModel { get; set; }
}

public class MatrixDistanceValue
{
    public int Value { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class MatrixElement
{
    public string Status { get; set; } = string.Empty;
    public MatrixDistanceValue? Distance { get; set; }
    public MatrixDistanceValue? Duration { get; set; }
    public MatrixDistanceValue? DurationInTraffic { get; set; }
    public double? FarePrice { get; set; }
    public string? FareCurrency { get; set; }
}

public class MatrixRow
{
    public List<MatrixElement> Elements { get; set; } = new();
}

public class DistanceMatrixResponse
{
    public string Status { get; set; } = string.Empty;
    public List<string> OriginAddresses { get; set; } = new();
    public List<string> DestinationAddresses { get; set; } = new();
    public List<MatrixRow> Rows { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

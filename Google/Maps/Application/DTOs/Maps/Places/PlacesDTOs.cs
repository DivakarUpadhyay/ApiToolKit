using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Maps.Geocoding;

namespace ApiToolKit.Application.DTOs.Maps.Places;

public class NearbySearchRequest
{
    [Required]
    public double Lat { get; set; }
    [Required]
    public double Lng { get; set; }
    public int Radius { get; set; } = 1000;
    public string? Keyword { get; set; }
    public string? Type { get; set; }
    public string Language { get; set; } = "en";
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? OpenNow { get; set; }
    public string? PageToken { get; set; }
}

public class TextSearchRequest
{
    [Required]
    public string Query { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public string? Type { get; set; }
    public int? Radius { get; set; }
    public double? Lat { get; set; }
    public double? Lng { get; set; }
    public string? PageToken { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? OpenNow { get; set; }
}

public class PlaceDetailsRequest
{
    [Required]
    public string PlaceId { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public string Fields { get; set; } = "*";
    public string? SessionToken { get; set; }
}

public class PlaceAutoCompleteRequest
{
    [Required]
    public string Input { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public string? Types { get; set; }
    public string? Components { get; set; }
    public double? Lat { get; set; }
    public double? Lng { get; set; }
    public int? Radius { get; set; }
    public string? SessionToken { get; set; }
    public bool StrictBounds { get; set; } = false;
}

public class PlacePhoto
{
    public string PhotoReference { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Width { get; set; }
    public List<string> HtmlAttributions { get; set; } = new();
}

public class OpeningHours
{
    public bool? OpenNow { get; set; }
    public object? Periods { get; set; }
    public List<string> WeekdayText { get; set; } = new();
    public bool? PermanentlyClosed { get; set; }
}

public class PlaceReview
{
    public string AuthorName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string? RelativeTimeDescription { get; set; }
    public string? Text { get; set; }
    public long Time { get; set; }
    public string? Language { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public string? AuthorUrl { get; set; }
}

public class PlaceResult2
{
    public string PlaceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? FormattedAddress { get; set; }
    public string? FormattedPhoneNumber { get; set; }
    public string? InternationalPhoneNumber { get; set; }
    public double? Rating { get; set; }
    public int? UserRatingsTotal { get; set; }
    public List<string> Types { get; set; } = new();
    public string? Vicinity { get; set; }
    public string? Website { get; set; }
    public string? BusinessStatus { get; set; }
    public GeocodeGeometry? Geometry { get; set; }
    public OpeningHours? OpeningHours { get; set; }
    public List<PlacePhoto> Photos { get; set; } = new();
    public List<PlaceReview> Reviews { get; set; } = new();
    public int? PriceLevel { get; set; }
    public bool? PermanentlyClosed { get; set; }
    public string? Url { get; set; }
    public int? UtcOffset { get; set; }
    public object? Plus_Code { get; set; }
}

public class PlacesResponse
{
    public string Status { get; set; } = string.Empty;
    public List<PlaceResult2> Results { get; set; } = new();
    public string? NextPageToken { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string> HtmlAttributions { get; set; } = new();
}

public class PlaceAutocompletePrediction
{
    public string Description { get; set; } = string.Empty;
    public string PlaceId { get; set; } = string.Empty;
    public PlaceStructuredFormatting? StructuredFormatting { get; set; }
    public List<string> Types { get; set; } = new();
    public int? DistanceMeters { get; set; }
}

public class PlaceStructuredFormatting
{
    public string MainText { get; set; } = string.Empty;
    public string? SecondaryText { get; set; }
    public object? MainTextMatchedSubstrings { get; set; }
}

public class PlaceAutocompleteResponse
{
    public string Status { get; set; } = string.Empty;
    public List<PlaceAutocompletePrediction> Predictions { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

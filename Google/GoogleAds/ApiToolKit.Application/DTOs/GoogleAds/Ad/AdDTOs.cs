using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Ad;

public class ListAdsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    public string? AdGroupResourceName { get; set; }
    public int PageSize { get; set; } = 50;
}

public class AdResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string AdGroupResourceName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<string> Headlines { get; set; } = new();
    public List<string> Descriptions { get; set; } = new();
    public List<string> FinalUrls { get; set; } = new();
}

public class AdListResponse
{
    public List<AdResponse> Ads { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class CreateResponsiveSearchAdRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string AdGroupResourceName { get; set; } = string.Empty;
    [Required] public List<string> Headlines { get; set; } = new();
    [Required] public List<string> Descriptions { get; set; } = new();
    [Required] public List<string> FinalUrls { get; set; } = new();
}

public class UpdateAdRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string AdGroupAdResourceName { get; set; } = string.Empty;
    public string? Status { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.BiddingStrategy;

public class ListBiddingStrategiesRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
}

public class BiddingStrategyResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int CampaignCount { get; set; }
    public int NonRemovedCampaignCount { get; set; }
}

public class CreateBiddingStrategyRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Type { get; set; } = string.Empty;
    public long? TargetCpaMicros { get; set; }
    public double? TargetRoas { get; set; }
}

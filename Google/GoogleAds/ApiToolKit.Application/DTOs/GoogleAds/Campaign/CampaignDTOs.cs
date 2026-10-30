using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Campaign;

public class ListCampaignsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
}

public class CampaignResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string AdvertisingChannelType { get; set; } = string.Empty;
    public string BiddingStrategyType { get; set; } = string.Empty;
    public long BudgetAmountMicros { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string ServingStatus { get; set; } = string.Empty;
}

public class CampaignListResponse
{
    public List<CampaignResponse> Campaigns { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class CreateCampaignRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string AdvertisingChannelType { get; set; } = "SEARCH";
    [Required] public long BudgetAmountMicros { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string BiddingStrategyType { get; set; } = "TARGET_CPA";
    /// <summary>CONTAINS_EU_POLITICAL_ADVERTISING or DOES_NOT_CONTAIN_EU_POLITICAL_ADVERTISING (required by Google for new campaigns)</summary>
    public string ContainsEuPoliticalAdvertising { get; set; } = "DOES_NOT_CONTAIN_EU_POLITICAL_ADVERTISING";
}

public class UpdateCampaignRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string ResourceName { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
    public long? BudgetAmountMicros { get; set; }
}

public class MutateOperationResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public int Index { get; set; }
}

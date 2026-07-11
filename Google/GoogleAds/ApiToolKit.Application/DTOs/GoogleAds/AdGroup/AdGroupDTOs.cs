using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.AdGroup;

public class ListAdGroupsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    public string? CampaignResourceName { get; set; }
    public int PageSize { get; set; } = 50;
}

public class AdGroupResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CampaignResourceName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long CpcBidMicros { get; set; }
}

public class AdGroupListResponse
{
    public List<AdGroupResponse> AdGroups { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class CreateAdGroupRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string CampaignResourceName { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "SEARCH_STANDARD";
    public long CpcBidMicros { get; set; }
}

public class UpdateAdGroupRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string ResourceName { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
    public long? CpcBidMicros { get; set; }
}
